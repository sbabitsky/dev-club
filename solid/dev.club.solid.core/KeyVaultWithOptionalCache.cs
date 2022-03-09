using System.Threading.Tasks;
using keyvault.abstractions;

namespace dev.club.solid.core
{
    public class KeyVaultWithOptionalCache : IKeyVault
    {
        private readonly IKeyVault _original;
        private readonly bool _enableCache;

        public KeyVaultWithOptionalCache(IKeyVault original, bool enableCache)
        {
            _original = original;
            _enableCache = enableCache;
        }

        public async Task<IKeyVaultClient> CreateClientAsync()
        {
            if (_enableCache)
            {
                return new KeyVaultClientWithCache(_original);
            }

            return await _original.CreateClientAsync();
        }
    }
}
