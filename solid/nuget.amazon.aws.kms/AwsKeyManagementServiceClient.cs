using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Amazon.Aws.Kms.Abstractions;

namespace Amazon.Aws.Kms
{
    public class AwsKeyManagementServiceClient : IAwsKeyManagementServiceClient
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<X509Certificate2> GetCertificate2Async(object any)
        {
            throw new NotImplementedException();
        }

        public Task<X509Certificate2> UploadCertificateAsync(object any)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCertificateAsync(object any)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetSecretAsync(object any)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSecretAsync(object any)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAnythingAsync(object any)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAnythingAsync(object any)
        {
            throw new NotImplementedException();
        }
    }
}
