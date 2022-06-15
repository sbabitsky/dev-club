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
            // use case model -> usage scenarios
            // 1. The system (the validator) will invoke GetCertificateAsync
            // for every single request to the WCF service (Q: How many request do we have per second?)
            // expectation for #1: Minimize the count of actual load from the "original" client (_original.GetCertificateAsync(thumbprint)).
            // 1 request per second - most likely you need a cache
            // 10 r/s - really nice to have cache
            // 100 r/s - must have cache
            // 0.1 r/s - 0.1 s - can consider cache
            // 0.01 r/s - 0.1 s - NO CACHE at all
            // 2. Many clients are using the service. (Q: But how many? How many different thumbprints?)
            // expecation: 100 clients, 100x3 = 300  (at maxim) - calculate the size of the cache - 30 MB
            // decision: use memory cache
            // 3. 1 Client (the same client) is calling the WCF Service MANY time with the same cert/thumbprint
            //expectation: cold start (nothing in a cache) -> load certificate and cache,
            // all subsequent request (for 60 minutes) will just use the value from the cache
            // 4. different clients a calling the service at EXACTLY the same time (cold start)
            // expectation: clientB does NOT wait until we will load the certificate for the clientA
            // 5. do NOT load the certificate for the same thumbprint TWICE
            // expectation: the certificate is loaded JUST ONCE and cached, 2nd call - use the value from the cache
            // 6. Thread Safety
            Thumbprint certificateKey = thumbprint;

            //1+
            //2+
            //3+
            //4+
            //5- -> +
            //6+
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

        public async Task<X509Certificate2> GetCertificateAsyncV2(string thumbprint)
        {
            // use case model -> usage scenarios
            // 1. The system (the validator) will invoke GetCertificateAsync
            // for every single request to the WCF service (Q: How many request do we have per second?)
            // expectation for #1: Minimize the count of actual load from the "original" client (_original.GetCertificateAsync(thumbprint)).
            // 1 request per second - most likely you need a cache
            // 10 r/s - really nice to have cache
            // 100 r/s - must have cache
            // 0.1 r/s - 0.1 s - can consider cache
            // 0.01 r/s - 0.1 s - NO CACHE at all
            // 2. Many clients are using the service. (Q: But how many? How many different thumbprints?)
            // expecation: 100 clients, 100x3 = 300  (at maxim) - calculate the size of the cache - 30 MB
            // decision: use memory cache
            // 3. 1 Client (the same client) is calling the WCF Service MANY time with the same cert/thumbprint
            //expectation: cold start (nothing in a cache) -> load certificate and cache,
            // all subsequent request (for 60 minutes) will just use the value from the cache
            // 4. different clients a calling the service at EXACTLY the same time (cold start)
            // expectation: clientB does NOT wait until we will load the certificate for the clientA
            // 5. do NOT load the certificate for the same thumbprint TWICE
            // expectation: the certificate is loaded JUST ONCE and cached, 2nd call - use the value from the cache
            // 6. Thread Safety
            Thumbprint certificateKey = thumbprint;

            //1+
            //2+
            //3+
            //4+
            //5- -> +
            //6+

            if (_cache.Contains(certificateKey))
            {
                return (X509Certificate2)_cache.Get(certificateKey);
            }

            // 4- 5 s
            // 5+
            lock (typeof(KeyVaultClientCacheDecorator))
            {
                if (_cache.Contains(certificateKey))
                {
                    return (X509Certificate2)_cache.Get(certificateKey);
                }

                var certificate = _original.GetCertificateAsync(thumbprint).GetAwaiter().GetResult();

                _cache.Add(certificateKey, certificate, new CacheItemPolicy
                {
                    SlidingExpiration = TimeSpan.FromHours(1)
                });

                return certificate;
            }
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
