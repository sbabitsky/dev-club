using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace keyvault.abstractions
{
    public interface IKeyVaultClient: ICertificateProvider, IDisposable
    {
        Task<X509Certificate2> UploadCertificateAsync(object any);

        Task DeleteCertificateAsync(object any);

        Task<object> GetSecretAsync(object any);

        Task DeleteSecretAsync(object any);

        Task<object> GetAnythingAsync(object any);

        Task DeleteAnythingAsync(object any);
    }

    public interface ICertificateProvider
    {
        Task<X509Certificate2> GetCertificateAsync(object any);
    }
}
