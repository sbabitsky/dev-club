using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using KeyVault.Abstractions;
using Microsoft.Azure.KeyVault.Abstractions;

namespace KeyVault.Microsoft.AzureKeyVault
{
    internal class AzureKeyVaultClient : IKeyVaultClient
    {
        private readonly IAzureKeyVaultClient _azureKeyVaultClient;

        public AzureKeyVaultClient(IAzureKeyVaultClient azureKeyVaultClient)
        {
            // authorization
            // request access token
            // 3600 => 6 min
            _azureKeyVaultClient = azureKeyVaultClient;
        }
        public Task<X509Certificate2> GetCertificateAsync(string thumbprint)
        {
            return _azureKeyVaultClient.GetCertificateAsync(thumbprint);
        }

        public void Dispose()
        {
            _azureKeyVaultClient.Dispose();
        }

        public Task<X509Certificate2> UploadCertificateAsync(object any)
        {
            return _azureKeyVaultClient.UploadCertificateAsync(any);
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
