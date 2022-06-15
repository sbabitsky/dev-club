using System.Security.Cryptography.X509Certificates;

namespace Dev.Club.CertificationAuthority.Abstractions
{
    // Role: Director
    public interface ICertificateAuthority
    {
        IssuedCertificateResponse IssueSelfSignedCertificate(IssueSelfSignedCertificateRequest request);

        IssuedCertificateResponse IssueSSLCertificate(IssueCertificateRequest request, CancellationToken cancellationToken);
    }

    public class IssueSelfSignedCertificateRequest
    {
        public string Subject { get; set; }
    }

    public class IssueCertificateRequest
    {
        public string Subject { get; set; }

        public string FriendlyName { get; set; }
    }

    public class IssuedCertificateResponse
    {
        public X509Certificate2 Certificate { get; set; }
    }
}