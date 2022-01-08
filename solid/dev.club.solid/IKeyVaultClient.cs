using System.Security.Cryptography.X509Certificates;

namespace dev.club.solid
{
    internal interface IKeyVaultClient: IDisposable
    {
        Task<X509Certificate2> GetCertificateAsync(object any);

        Task DeleteCertificateAsync(object any);

        Task<object> GetSecretAsync(object any);

        Task DeleteSecretAsync(object any);

        Task<object> GetAnythingAsync(object any);

        Task DeleteAnythingAsync(object any);
    }
}
