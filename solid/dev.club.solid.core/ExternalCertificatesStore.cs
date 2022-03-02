using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using dev.club.solid.azurekeyvault.abstractions;

namespace dev.club.solid.core
{
    public class ExternalCertificatesStore : ICertificatesStore
    {
        private readonly IAzureKeyVault _azureKeyVault;
        private readonly IDictionary<Thumbprint, string> _certificateMapping;

        public ExternalCertificatesStore(IAzureKeyVault azureKeyVault, ExchangeConfiguration exchangeConfiguration)
        {
            _azureKeyVault = azureKeyVault;

            try
            {
                _certificateMapping = exchangeConfiguration.CertificateMappings
                    .ToDictionary(x => (Thumbprint)x.Thumbprint, x => x.UniqueId);
            }
            catch
            {
                // log the real exception
                throw new InvalidOperationException($"The configuration is invalid.");
            }
        }

        public async Task<X509Certificate2> GetCertificateAsync(Thumbprint thumbprint)
        {
            if (IsTheCertificateIsStoredInAzure(thumbprint, out string uniqueId))
            {
                using (var client = await _azureKeyVault.CreateClientAsync())
                {
                    return await client.GetCertificateAsync(uniqueId);
                }
            }

            // new X509Store(StoreLocation.LocalMachine).Certificates;
            // default / existing flow
            return null!;
        }

        public bool IsTheCertificateIsStoredInAzure(Thumbprint thumbprint, out string uniqueId)
        {
            return _certificateMapping.TryGetValue(thumbprint, out uniqueId);
        }
    }
}
