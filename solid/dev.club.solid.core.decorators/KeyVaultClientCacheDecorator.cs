using System.Collections.Concurrent;
using System.Runtime.Caching;
using System.Security.Cryptography.X509Certificates;
using KeyVault.Abstractions;

namespace Dev.Club.Solid.Core.Decorators
{
    public class KeyVaultClientCacheDecorator : IKeyVaultClient
    {
        private readonly IKeyVault _original;

        private readonly ObjectCache _cache;

        public KeyVaultClientCacheDecorator(IKeyVault original, ObjectCache cache)
        {
            _original = original;
            _cache = cache;
        }

        public async Task<X509Certificate2> GetCertificateAsync(string thumbprint)
        {
            Thumbprint certificateKey = thumbprint;

            if (_cache.Contains(certificateKey))
            {
                return (X509Certificate2)_cache.Get(certificateKey);
            }

            using var client = await _original.CreateClientAsync();

            lock (string.Intern(string.Concat(nameof(KeyVaultClientCacheDecorator), thumbprint)))
            {
                if (_cache.Contains(certificateKey))
                {
                    return (X509Certificate2)_cache.Get(certificateKey);
                }

                var certificate = client.GetCertificateAsync(thumbprint).GetAwaiter().GetResult();

                _cache.Add(certificateKey, certificate, new CacheItemPolicy
                {
                    SlidingExpiration = TimeSpan.FromHours(1)
                });

                // Using Cache as a Whitelist // about memory

                return certificate;
            }
        }

        public void Dispose()
        {
        }

        public async Task<X509Certificate2> UploadCertificateAsync(object any)
        {
            using var client = await _original.CreateClientAsync();
            return await client.UploadCertificateAsync(any);
        }

        public Task DeleteCertificateAsync(object any)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> GetSecretAsync(object any)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteSecretAsync(object any)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> GetAnythingAsync(object any)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAnythingAsync(object any)
        {
            throw new System.NotImplementedException();
        }
    }
}
