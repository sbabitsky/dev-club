using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Amazon.Aws.Kms.Abstractions
{
    public interface IAwsKeyManagementServiceClient: IDisposable
    {
        Task<X509Certificate2> GetCertificate2Async(object any);

        Task<X509Certificate2> UploadCertificateAsync(object any);

        Task DeleteCertificateAsync(object any);

        Task<object> GetSecretAsync(object any);

        Task DeleteSecretAsync(object any);

        Task<object> GetAnythingAsync(object any);

        Task DeleteAnythingAsync(object any);
    }
}
