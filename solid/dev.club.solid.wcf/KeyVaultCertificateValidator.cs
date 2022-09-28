using System;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using Dev.Club.Solid.Core;
using Microsoft.Extensions.Logging;

namespace dev.club.solid.wcf
{
    public class KeyVaultCertificateValidator : X509CertificateValidator
    {
        private readonly ILogger _logger;
        private readonly ICertificatesStore _certificatesStore;

        // key - thumbprint
        // value - unique id
        public KeyVaultCertificateValidator(ICertificatesStore certificatesStore, ILogger<KeyVaultCertificateValidator> logger)
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
                var message = $"Failed to retrieve certificate with thumbprint {certificate.Thumbprint} from KeyVault: {ex.Message}";

                _logger.LogError(ex, "Failed to retrieve certificate with {Thumbprint} from KeyVault. {ErrorMessage}", certificate.Thumbprint, ex.Message);
                
                throw new SecurityTokenValidationException(
                    message,
                    ex);
            }
        }
    }
}
