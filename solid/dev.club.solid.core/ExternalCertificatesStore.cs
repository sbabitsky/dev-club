using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using dev.club.solid.azurekeyvault.abstractions;

namespace dev.club.solid.core
{
    public class ExternalCertificatesStore : ICertificatesStore
    {
        private readonly IAzureKeyVault _azureKeyVault;
        private readonly ExchangeConfiguration _exchangeConfiguration;

        public ExternalCertificatesStore(IAzureKeyVault azureKeyVault, ExchangeConfiguration exchangeConfiguration)
        {
            _azureKeyVault = azureKeyVault;
            _exchangeConfiguration = exchangeConfiguration;
        }

        public async Task<X509Certificate2> GetCertificateAsync(string thumbprint)
        {
            if (IsTheCertificateIsStoredInAzure(thumbprint, out string uniqueId))
            {
                // put cache
                using (var client = await _azureKeyVault.CreateClientAsync())
                {
                    return await client.GetCertificateAsync(uniqueId);
                }
            }

            // new X509Store(StoreLocation.LocalMachine).Certificates;
            // default / existing flow
            return null!;
        }

        public bool IsTheCertificateIsStoredInAzure(string thumbprint, out string uniqueId)
        {
            uniqueId = string.Empty;

            var matchingThumbprints = _exchangeConfiguration?.CertificateMappings
                .Where(item => item.Thumbprint.ToUpperInvariant() == thumbprint.ToUpperInvariant());

            if (matchingThumbprints?.Count() > 1)
            {
                throw new InvalidOperationException($"More than one client certificate thumbprints found in the config service for {thumbprint}");
            }

            if (matchingThumbprints?.Count() == 1)
            {
                uniqueId = matchingThumbprints.First().UniqueId;

                return true;
            }

            return false;
        }
    }
}
