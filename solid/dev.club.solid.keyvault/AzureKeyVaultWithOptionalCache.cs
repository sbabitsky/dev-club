using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using dev.club.solid.azurekeyvault.abstractions;

namespace dev.club.solid.azurekeyvault
{
    public class AzureKeyVaultWithOptionalCache : IAzureKeyVault
    {
        private readonly IAzureKeyVault _original;
        private readonly bool _enableCache;

        public AzureKeyVaultWithOptionalCache(IAzureKeyVault original, bool enableCache)
        {
            _original = original;
            _enableCache = enableCache;
        }

        public async Task<IAzureKeyVaultClient> CreateClientAsync()
        {
            if (_enableCache)
            {
                return new AzureKeyVaultClientWithCache(_original);
            }

            return await _original.CreateClientAsync();
        }
    }
}
