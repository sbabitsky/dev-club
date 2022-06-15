using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Dev.Club.CertificationAuthority.Abstractions;

namespace Dev.Club.CertificationAuthority
{
    internal class CloudCertificateBuilder : ICertificateBuilder, ICertificateBuilderFinalStep, ICloneable
    {
        private string _friendlyName;
        private CertificateUsageType _certificateUsageType;
        private string _subject;
        private DateTimeOffset? _notBefore;
        private DateTimeOffset? _notAfter;

        private string _accessToken;
        private string _applicationId;


        public CloudCertificateBuilder(string accessToken)
        {
            _accessToken = accessToken;
            _applicationId = GetApplicationIdFromTheConfigurationService();
        }

        private CloudCertificateBuilder(CloudCertificateBuilder prototype)
        {
            _accessToken = prototype._accessToken;
            _applicationId = prototype._applicationId;
        }

        public string GetApplicationIdFromTheConfigurationService()
        {
            // invoke REST API to get the current application id
            Thread.Sleep(5000);

            return Guid.NewGuid().ToString();
        }

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
            // TODO use configuration
            // Invoke some service
            using var ecdsa = ECDsa.Create();
            var req = new CertificateRequest(_subject, ecdsa, HashAlgorithmName.SHA256);
            var certificate = req.CreateSelfSigned(_notBefore ?? DateTimeOffset.Now, _notAfter ?? DateTimeOffset.Now.AddYears(1));

            certificate.FriendlyName = _friendlyName;

            return certificate;
        }

        public object Clone()
        {
            return new CloudCertificateBuilder(this);
        }
    }
}
