using dev.club.ca.abstractions;

namespace dev.club.ca
{
    public class LocalCertificateAuthority : ICertificateAuthority
    {
        public IssuedCertificateResponse IssueSelfSignedCertificate(IssueCertificateRequest request)
        {
            var certificateBuilder = DefaultCertificateBuilder.Create(request.Subject, DateTimeOffset.Now, DateTimeOffset.Now.AddYears(1));

            certificateBuilder
                .CertificateUsageType(CertificateUsageType.SelfSigned)
                .CalculateThumbprint();

            return new IssuedCertificateResponse
            {
                Certificate = certificateBuilder.Build()
            };
        }

        public IssuedCertificateResponse IssueSSLCertificate(IssueCertificateRequest request)
        {
            var certificateBuilder = DefaultCertificateBuilder.Create(request.Subject, DateTimeOffset.Now, DateTimeOffset.Now.AddYears(1));

            certificateBuilder
                .FriendlyName(request.FriendlyName)
                .CertificateUsageType(CertificateUsageType.SSL)
                .CalculateThumbprint();

            return new IssuedCertificateResponse
            {
                Certificate = certificateBuilder.Build()
            };
        }
    }
}
