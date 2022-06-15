using System.Runtime.Caching;
using System.Security.Cryptography.X509Certificates;
using KeyVault.Abstractions;

namespace Dev.Club.Solid.Core.Decorators
{
    public class KeyVaultClientCacheDecorator : IKeyVaultClient
    {
        private readonly IKeyVaultClient _original;

        private readonly ObjectCache _cache;

        public KeyVaultClientCacheDecorator(IKeyVaultClient original, ObjectCache cache)
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
            
            var certificate = await _original.GetCertificateAsync(thumbprint);

            _cache.Add(certificateKey, certificate, new CacheItemPolicy
            {
                SlidingExpiration = TimeSpan.FromHours(1)
            });

            return certificate;
        }

        public void Dispose()
        {
            _original.Dispose();
        }

        public Task<X509Certificate2> UploadCertificateAsync(object any)
        {
            return _original.UploadCertificateAsync(any);
        }

        public Task DeleteCertificateAsync(object any)
        {
            return _original.DeleteCertificateAsync(any);
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
