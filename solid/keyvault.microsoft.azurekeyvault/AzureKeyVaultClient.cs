using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using dev.club.solid.azurekeyvault.abstractions;
using keyvault.abstractions;
using nuget.microsoft.azurekeyvault.abstractions;

namespace keyvault.microsoft.azurekeyvault
{
    internal class AzureKeyVaultClient : IKeyVaultClient
    {
        private readonly IAzureKeyVaultClient _azureKeyVaultClient;

        public AzureKeyVaultClient(IAzureKeyVaultClient azureKeyVaultClient)
        {
            _azureKeyVaultClient = azureKeyVaultClient;
        }
        public Task<X509Certificate2> GetCertificateAsync(object any)
        {
            return _azureKeyVaultClient.GetCertificateAsync(any);
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
