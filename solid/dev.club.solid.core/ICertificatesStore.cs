using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Dev.Club.Solid.Core
{
    public interface ICertificatesStore
    {
        Task<X509Certificate2> GetCertificateAsync(Thumbprint thumbprint);
    }
}
