using System.Security.Cryptography.X509Certificates;

namespace dev.club.ca.abstractions
{
    // Role: Director
    public interface ICertificateAuthority
    {
        IssuedCertificateResponse IssueSelfSignedCertificate(IssueCertificateRequest request);

        IssuedCertificateResponse IssueSSLCertificate(IssueCertificateRequest request, CancellationToken cancellationToken);
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