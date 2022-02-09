using System;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using dev.club.solid.core;
using Microsoft.Extensions.Logging;

namespace dev.club.solid.wcf
{
    public class KeyVaultCertificateValidator : X509CertificateValidator
    {
        private readonly ILogger _logger;
        private readonly ICertificatesStore _certificatesStore;

        // key - thumbprint
        // value - unique id
        public KeyVaultCertificateValidator(ICertificatesStore certificatesStore, ILogger logger)
        {
            _certificatesStore = certificatesStore;
            _logger = logger;
        }

        public override void Validate(X509Certificate2 certificate)
        {
            try
            {
                var keyVaultCertificate = _certificatesStore.GetCertificateAsync(certificate.Thumbprint!).GetAwaiter().GetResult();

                if (keyVaultCertificate == null)
                {
                    throw new SecurityTokenValidationException($"Client certificate with thumbprint {certificate.Thumbprint} not found in KeyVault");
                }

                if (!keyVaultCertificate.Equals(certificate))
                {
                    throw new SecurityTokenValidationException($"Client certificate with thumbprint {certificate.Thumbprint} is invalid in KeyVault");
                }
            }
            catch (Exception ex)
            {
                throw new SecurityTokenValidationException(
                    $"Failed to retrieve certificate with thumbprint {certificate.Thumbprint} from KeyVault: {ex.Message}",
                    ex);
            }
        }
    }
}
