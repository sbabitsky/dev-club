using System;
using System.Threading.Tasks;

namespace dev.club.solid.keyvault
{
    public class AzureKeyVault : IKeyVault
    {
        public Task<IKeyVaultClient> CreateClientAsync()
        {
            throw new NotImplementedException();
        }
    }
}
