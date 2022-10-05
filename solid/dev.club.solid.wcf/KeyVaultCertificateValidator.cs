using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using Dev.Club.Solid.Core;
using Microsoft.Extensions.Logging;

namespace dev.club.solid.wcf
{
    public class KeyVaultCertificateValidator : X509CertificateValidator
    {
        private static readonly Action<ILogger, string, string, Exception> _failedToRetrieveCertificateError = LoggerMessage.Define<string, string>(
            LogLevel.Error,
            new EventId(1, nameof(Validate)),
            "Failed to retrieve certificate with {Thumbprint} from KeyVault. {ErrorMessage}");

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
            using (_logger.BeginScope(new Dictionary<string, object>
                   {
                       ["Thumbprint"] = certificate.Thumbprint
                   }))
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

                    //_failedToRetrieveCertificateError(_logger, certificate.Thumbprint, ex.Message, ex);

                    _logger.LogError(ex, "Failed to retrieve certificate {Thumbprint} from KeyVault.");

                    throw new SecurityTokenValidationException(
                        message,
                        ex);
                }
            }
        }
    }
}
