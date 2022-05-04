using System.Security.Cryptography.X509Certificates;

namespace dev.club.ca.abstractions
{
    public interface ICertificateBuilder
    {
        ICertificateBuilder FriendlyName(string friendlyName);

        ICertificateBuilder CertificateUsageType(CertificateUsageType certificateUsageType);

        ICertificateBuilder CalculateThumbprint();

        X509Certificate2 Build();
    }
}
