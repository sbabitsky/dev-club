using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using dev.club.ca.abstractions;

namespace dev.club.ca
{
    internal class EnhancedCertificateBuilder : ICertificateBuilder, ICertificateBuilderFinalStep
    {
        private string _friendlyName;
        private CertificateUsageType _certificateUsageType;
        private string _subject;
        private DateTimeOffset? _notBefore;
        private DateTimeOffset? _notAfter;

        public ICertificateBuilder Subject(string subject)
        {
            _subject = subject;

            return this;
        }

        public ICertificateBuilder NotBefore(DateTimeOffset notBefore)
        {
            _notBefore = notBefore;

            return this;
        }

        public ICertificateBuilder NotAfter(DateTimeOffset notAfter)
        {
            _notAfter = notAfter;

            return this;
        }

        public ICertificateBuilder FriendlyName(string friendlyName)
        {
            _friendlyName = friendlyName;

            return this;
        }

        public ICertificateBuilder CertificateUsageType(CertificateUsageType certificateUsageType)
        {
            _certificateUsageType = certificateUsageType;

            return this;
        }

        public ICertificateBuilderFinalStep Validate()
        {
            if (string.IsNullOrEmpty(_subject))
            {
                throw new InvalidOperationException("Subject is required.");
            }

            return this;
        }

        public X509Certificate2 Build()
        {
            using var ecdsa = ECDsa.Create();
            var req = new CertificateRequest(_subject, ecdsa, HashAlgorithmName.SHA256);
            return req.CreateSelfSigned(_notBefore ?? DateTimeOffset.Now, _notAfter ?? DateTimeOffset.Now.AddYears(1));
            // invoke Azure
        }
    }
}
