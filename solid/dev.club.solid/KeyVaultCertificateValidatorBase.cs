using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using dev.club.solid.Hidden;
using Microsoft.Extensions.Logging;

namespace dev.club.solid
{
    internal abstract class KeyVaultCertificateValidatorBase : System.IdentityModel.Selectors.X509CertificateValidator
    {
        public abstract IDictionary<string, string> GetExchangeIntegrationConfiguration();

        public abstract IKeyVault GetKeyVault();

        public abstract ILogger GetLogger();

        public override void Validate(X509Certificate2 certificate)
        {
            var logger = GetLogger();

            var configDictionary = GetExchangeIntegrationConfiguration();
            var matchingThumbprints = configDictionary?.Where(item => item.Key.ToUpperInvariant() == certificate.Thumbprint.ToUpperInvariant());

            if (matchingThumbprints?.Count() > 1)
            {
                throw new SecurityTokenValidationException($"More than one client certificate thumbprints found in the config service for {certificate.Thumbprint}");
            }

            if (matchingThumbprints?.Count() == 1)
            {
                logger.LogInformation($"Client certificate thumbprint {certificate.Thumbprint} found in the config service");

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

                    logger.LogInformation($"Client certificate with thumbprint {certificate.Thumbprint} successfully validated in WexKeyStore");
                    return;
                }
                catch (Exception ex)
                {
                    throw new SecurityTokenValidationException(
                        $"Failed to retrieve certificate with thumbprint {certificate.Thumbprint} from WexKeyStore: {ex.Message}",
                        ex);
                }
            }

            logger.LogInformation($"Client certificate thumbprint {certificate.Thumbprint} not found in the config service. Will proceed with default validation");

            ValidateCertificate(DefaultX509CertificateValidator.PeerOrChainTrust, certificate);

            logger.LogInformation($"Client certificate with thumbprint {certificate.Thumbprint} successfuly validated in the default storage");
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
            using (var client = await GetKeyVault().CreateClientAsync())
            {
                return await client.GetCertificateAsync(uuid);
            }
        }

    }
}
