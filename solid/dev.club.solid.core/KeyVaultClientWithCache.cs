using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using keyvault.abstractions;

namespace dev.club.solid.core
{
    public class KeyVaultClientWithCache : IKeyVaultClient
    {
        private readonly IKeyVault _keyVault;
        private readonly object MemoryCache; 

        public KeyVaultClientWithCache(IKeyVault keyVault)
        {
            _keyVault = keyVault;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<X509Certificate2> GetCertificateAsync(object any)
        {
            //if found in cache
            //return from cache

            using (var client = await _keyVault.CreateClientAsync())
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
