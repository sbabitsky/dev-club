using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;

namespace dev.club.solid.Hidden
{
    internal class DefaultX509CertificateValidator : X509CertificateValidator
    {
        public override void Validate(X509Certificate2 certificate)
        {
            
        }

        public static X509CertificateValidator PeerOrChainTrust { get; }
    }
}
