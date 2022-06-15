using System.Security.Cryptography.X509Certificates;

namespace Dev.Club.CertificationAuthority.Abstractions
{
    public interface ICertificateBuilder
    {
        ICertificateBuilder Subject(string subject);

        ICertificateBuilder NotBefore(DateTimeOffset notBefore);

        ICertificateBuilder NotAfter(DateTimeOffset notAfter);

        ICertificateBuilder FriendlyName(string friendlyName);

        ICertificateBuilder CertificateUsageType(CertificateUsageType certificateUsageType);

        ICertificateBuilderFinalStep Validate();
    }

    public interface ICertificateBuilderFinalStep
    {
        X509Certificate2 Build();
    }
}
