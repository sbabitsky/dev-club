using dev.club.ca.abstractions;

namespace dev.club.ca
{
    // Role: Director
    public class LocalCertificateAuthority : ICertificateAuthority
    {
        private readonly ICertificateBuilder _certificateBuilder;


        // client code
        public static void Main()
        {
            var certificateAuthority = new LocalCertificateAuthority(new EnhancedCertificateBuilder());

            //certificateAuthority.IssueSSLCertificate()
        }

        public LocalCertificateAuthority(ICertificateBuilder certificateBuilder)
        {
            _certificateBuilder = certificateBuilder;
        }

        public IssuedCertificateResponse IssueSelfSignedCertificate(IssueCertificateRequest request)
        {
            _certificateBuilder
                .Subject(request.Subject)
                .CertificateUsageType(CertificateUsageType.SelfSigned);

            return new IssuedCertificateResponse
            {
                Certificate = _certificateBuilder.Validate().Build()
            };
        }

        public IssuedCertificateResponse IssueSSLCertificate(IssueCertificateRequest request, CancellationToken cancellationToken)
        {
            _certificateBuilder
                .Subject(request.Subject)
                .FriendlyName(request.FriendlyName)
                .CertificateUsageType(CertificateUsageType.SSL)
                .NotAfter(DateTimeOffset.Now)
                .NotBefore(DateTimeOffset.Now);

            if (cancellationToken.IsCancellationRequested)
            {
                return new IssuedCertificateResponse();
            }

            return new IssuedCertificateResponse
            {
                Certificate = _certificateBuilder.Validate().Build()
            };
        }
    }
}
