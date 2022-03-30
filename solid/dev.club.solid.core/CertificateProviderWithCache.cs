using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using keyvault.abstractions;

namespace dev.club.solid.core
{
    public class CertificateProviderWithCache : ICertificateProvider
    {
        private readonly IKeyVault _original;

        public CertificateProviderWithCache(IKeyVault original)
        {
            _original = original;
        }

        public async Task<X509Certificate2> GetCertificateAsync(object any)
        {
            //if found in cache
            //return from cache
            // TODO implement

            using (var client = await _original.CreateClientAsync())
            {
                return await client.GetCertificateAsync(any);
            }
        }
    }
}
