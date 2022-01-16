using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using dev.club.solid.Hidden;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace dev.club.solid
{
    public class KeyVaultCertificateValidator : X509CertificateValidator
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public KeyVaultCertificateValidator(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public override void Validate(X509Certificate2 certificate)
        {
            var configDictionary = GetExchangeIntegrationConfiguration();
            var matchingThumbprints = configDictionary?.Where(item => item.Key.ToUpperInvariant() == certificate.Thumbprint.ToUpperInvariant());

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
                    var keyVaultCertificate = LoadKeyVaultCertificateAsync(uuid).GetAwaiter().GetResult();

                    if (keyVaultCertificate == null)
                    {
                        throw new SecurityTokenValidationException($"Client certificate with thumbprint {certificate.Thumbprint} not found in WexKeyStore");
                    }

                    if (!keyVaultCertificate.Equals(certificate))
                    {
                        throw new SecurityTokenValidationException($"Client certificate with thumbprint {certificate.Thumbprint} is invalid in WexKeyStore");
                    }

                    _logger.LogInformation($"Client certificate with thumbprint {certificate.Thumbprint} successfully validated in WexKeyStore");
                    return;
                }
                catch (Exception ex)
                {
                    throw new SecurityTokenValidationException(
                        $"Failed to retrieve certificate with thumbprint {certificate.Thumbprint} from WexKeyStore: {ex.Message}",
                        ex);
                }
            }

            _logger.LogInformation($"Client certificate thumbprint {certificate.Thumbprint} not found in the config service. Will proceed with default validation");

            ValidateCertificate(DefaultX509CertificateValidator.PeerOrChainTrust, certificate);

            _logger.LogInformation($"Client certificate with thumbprint {certificate.Thumbprint} successfuly validated in the default storage");
        }

        private IDictionary<string, string> GetExchangeIntegrationConfiguration()
        {
            return _configuration.GetSection("KeyVault:ExchangeIntegrationConfiguration")
                .Get<IDictionary<string, string>>();
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
            using (var client = await new AzureKeyVault().CreateClientAsync())
            {
                return await client.GetCertificateAsync(uuid);
            }
        }

    }
}
