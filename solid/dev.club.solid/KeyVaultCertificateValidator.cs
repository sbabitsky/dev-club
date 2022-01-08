using Microsoft.Extensions.Logging;

namespace dev.club.solid
{
    internal class KeyVaultCertificateValidator : KeyVaultCertificateValidatorBase
    {
        public override IDictionary<string, string> GetExchangeIntegrationConfiguration()
        {
            return null!; // exchange integration from DI
        }

        public override IKeyVault GetKeyVault()
        {
            return null!; // key vault from DI
        }

        public override ILogger GetLogger()
        {
            return null!; // logger from DI
        }
    }
}
