using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using dev.club.ca.abstractions;

namespace dev.club.ca
{
    public class DefaultCertificateBuilder : ICertificateBuilder, ICertificateBuilderFinalStep
    {
        private readonly X509Certificate2 _certificate;

        private DefaultCertificateBuilder(X509Certificate2 certificate)
        {
            _certificate = certificate ?? throw new InvalidOperationException("A certificate must be created first.");
        }

        public static ICertificateBuilder Create(string subject, DateTimeOffset notBefore, DateTimeOffset notAfter)
        {
            using var ecdsa = ECDsa.Create();
            var req = new CertificateRequest(subject, ecdsa, HashAlgorithmName.SHA256);
            var certificate = req.CreateSelfSigned(DateTimeOffset.Now, DateTimeOffset.Now.AddYears(5));

            return new DefaultCertificateBuilder(certificate);
        }

        public ICertificateBuilder Subject(string subject)
        {
            return this;
        }

        public ICertificateBuilder NotBefore(DateTimeOffset notBefore)
        {
            return this;
        }

        public ICertificateBuilder NotAfter(DateTimeOffset notAfter)
        {
            return this;
        }

        public ICertificateBuilder FriendlyName(string friendlyName)
        {
            _certificate.FriendlyName = friendlyName;

            return this;
        }

        public ICertificateBuilder CertificateUsageType(CertificateUsageType certificateUsageType)
        {
            // to something
            return this;
        }

        public ICertificateBuilderFinalStep Validate()
        {
            if (string.IsNullOrEmpty(_certificate.Thumbprint))
            {
                throw new InvalidOperationException("Thumbprint is not auto-calculated.");
            }

            return this;
        }

        public X509Certificate2 Build()
        {
            return _certificate;
        }
    }
}
