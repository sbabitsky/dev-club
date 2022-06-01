﻿using System.Runtime.Caching;
using keyvault.abstractions;

namespace dev.club.solid.core.decorators
{
    public class KeyVaultCacheDecorator : IKeyVault
    {
        private readonly IKeyVault _original;
        private static readonly ObjectCache KeyVaultCache = new MemoryCache(nameof(KeyVaultCacheDecorator));

        public KeyVaultCacheDecorator(IKeyVault original)
        {
            _original = original;
        }

        public async Task<IKeyVaultClient> CreateClientAsync()
        {
            return new KeyVaultClientCacheDecorator(await _original.CreateClientAsync(), KeyVaultCache);
        }
    }
}
