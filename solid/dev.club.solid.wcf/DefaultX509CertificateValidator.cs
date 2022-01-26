using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;

namespace dev.club.solid.wcf
{
    internal class DefaultX509CertificateValidator : X509CertificateValidator
    {
        public DefaultX509CertificateValidator(string trustType)
        {
            
        }
        public override void Validate(X509Certificate2 certificate)
        {
            
        }

        public static X509CertificateValidator PeerOrChainTrust => new DefaultX509CertificateValidator(nameof(PeerOrChainTrust));
    }
}
