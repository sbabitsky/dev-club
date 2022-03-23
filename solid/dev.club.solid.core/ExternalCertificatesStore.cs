using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using keyvault.abstractions;

namespace dev.club.solid.core
{
    public class ExternalCertificatesStore : ICertificatesStore
    {
        private readonly Func<Task<ICertificateProvider>> _createCertificateProvider;
        private readonly IDictionary<Thumbprint, string> _certificateMapping;

        public ExternalCertificatesStore(Func<Task<ICertificateProvider>> createCertificateProvider, ExchangeConfiguration exchangeConfiguration)
        {
            _createCertificateProvider = createCertificateProvider;

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
            if (IsTheCertificateIsStoredInTheKeyVault(thumbprint, out string uniqueId))
            {
                using var certificateProvider = await _createCertificateProvider();
                
                return await certificateProvider.GetCertificateAsync(uniqueId);
            }

            // new X509Store(StoreLocation.LocalMachine).Certificates;
            // default / existing flow
            return null!;
        }

        public bool IsTheCertificateIsStoredInTheKeyVault(Thumbprint thumbprint, out string uniqueId)
        {
            return _certificateMapping.TryGetValue(thumbprint, out uniqueId);
        }

    }
}
