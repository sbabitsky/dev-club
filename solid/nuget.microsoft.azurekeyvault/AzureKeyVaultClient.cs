using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using dev.club.solid.azurekeyvault.abstractions;
using nuget.microsoft.azurekeyvault.abstractions;

namespace nuget.microsoft.azurekeyvault
{
    public class AzureKeyVaultClient : IAzureKeyVaultClient
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<X509Certificate2> GetCertificateAsync(object any)
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
