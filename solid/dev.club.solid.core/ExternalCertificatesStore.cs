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
        private readonly ICertificateProvider _certificateProvider;
        private readonly IDictionary<Thumbprint, string> _certificateMapping;

        public ExternalCertificatesStore(ICertificateProvider certificateProvider, ExchangeConfiguration exchangeConfiguration)
        {
            _certificateProvider = certificateProvider;

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
                return await _certificateProvider.GetCertificateAsync(uniqueId);
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
