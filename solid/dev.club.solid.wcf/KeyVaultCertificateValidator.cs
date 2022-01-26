using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using dev.club.solid.azurekeyvault;
using Microsoft.Extensions.Logging;

namespace dev.club.solid.wcf
{
    public class KeyVaultCertificateValidator : X509CertificateValidator
    {
        private readonly ILogger _logger;
        private IAzureKeyVault _azureKeyVault;
        private readonly ExchangeConfiguration _exchangeConfiguration;

        // key - thumbprint
        // value - unique id
        public KeyVaultCertificateValidator(IAzureKeyVault azureKeyVault, ExchangeConfiguration exchangeConfiguration, ILogger logger)
        {
            _azureKeyVault = azureKeyVault;
            _exchangeConfiguration = exchangeConfiguration;
            _logger = logger;
        }

        // Resp - 0
        public override void Validate(X509Certificate2 certificate)
        {
            var matchingThumbprints = _exchangeConfiguration?.CertificateMappings.Where(item => item.Thumbprint.ToUpperInvariant() == certificate.Thumbprint.ToUpperInvariant());

            if (matchingThumbprints?.Count() > 1)
            {
                throw new SecurityTokenValidationException($"More than one client certificate thumbprints found in the config service for {certificate.Thumbprint}");
            }

            if (matchingThumbprints?.Count() == 1)
            {
                _logger.LogInformation($"Client certificate thumbprint {certificate.Thumbprint} found in the exchange integration configuration");

                var uuid = matchingThumbprints.First().Value;

                try
                {
                    // Resp - 2
                    var keyVaultCertificate = LoadKeyVaultCertificateAsync(uuid).GetAwaiter().GetResult();

                    if (keyVaultCertificate == null)
                    {
                        throw new SecurityTokenValidationException($"Client certificate with thumbprint {certificate.Thumbprint} not found in KeyVault");
                    }

                    if (!keyVaultCertificate.Equals(certificate))
                    {
                        throw new SecurityTokenValidationException($"Client certificate with thumbprint {certificate.Thumbprint} is invalid in KeyVault");
                    }

                    _logger.LogInformation($"Client certificate with thumbprint {certificate.Thumbprint} successfully validated in KeyVault");
                    return;
                }
                catch (Exception ex)
                {
                    throw new SecurityTokenValidationException(
                        $"Failed to retrieve certificate with thumbprint {certificate.Thumbprint} from KeyVault: {ex.Message}",
                        ex);
                }
            }

            _logger.LogInformation($"Client certificate thumbprint {certificate.Thumbprint} not found in the config service. Will proceed with default validation");

            ValidateCertificate(DefaultX509CertificateValidator.PeerOrChainTrust, certificate);

            _logger.LogInformation($"Client certificate with thumbprint {certificate.Thumbprint} successfuly validated in the default storage");
        }

        internal virtual void ValidateCertificate(X509CertificateValidator x509CertificateValidator, X509Certificate2 certificate)
        {
            try
            {
                x509CertificateValidator.Validate(certificate);
            }
            catch (Exception ex)
            {
                throw new SecurityTokenValidationException(
                    $"Default validation for client certificate with thumbprint {certificate.Thumbprint} failed: {ex.Message}",
                    ex);
            }
        }

        private async Task<X509Certificate2> LoadKeyVaultCertificateAsync(string uuid)
        {
            using (var client = await _azureKeyVault.CreateClientAsync())
            {
                // Resp - 4 - load certificate
                return await client.GetCertificateAsync(uuid);
            }
        }

    }
}
