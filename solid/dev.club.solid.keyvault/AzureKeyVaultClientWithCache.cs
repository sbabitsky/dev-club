using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using dev.club.solid.azurekeyvault.abstractions;

namespace dev.club.solid.azurekeyvault
{
    public class AzureKeyVaultClientWithCache : IAzureKeyVaultClient
    {
        private readonly IAzureKeyVault _azureKeyVault;
        private readonly object MemoryCache; 

        public AzureKeyVaultClientWithCache(IAzureKeyVault azureKeyVault)
        {
            _azureKeyVault = azureKeyVault;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<X509Certificate2> GetCertificateAsync(object any)
        {
            //if found in cache
            //return from cache

            using (var client = await _azureKeyVault.CreateClientAsync())
            {
                return await client.GetCertificateAsync(any);
            }
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
