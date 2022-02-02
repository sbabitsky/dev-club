using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace dev.club.solid.core
{
    public interface ICertificatesStore
    {
        Task<X509Certificate2> GetCertificateAsync(string thumbprint);
    }
}
