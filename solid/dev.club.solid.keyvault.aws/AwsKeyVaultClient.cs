using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using nuget.amazon.aws.keyvault.abstractions;

namespace nuget.amazon.aws.keyvault
{
    public class AwsKeyVaultClient : IAwsKeyVaultClient
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<X509Certificate2> GetCertificateSomehowDifferentAsync(object any)
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
