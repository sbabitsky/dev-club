using System;
using System.Threading.Tasks;
using keyvault.abstractions;

namespace keyvault.microsoft.azurekeyvault
{
    public class AzureKeyVault : IKeyVault
    {
        public Task<IKeyVaultClient> CreateClientAsync()
        {
            throw new NotImplementedException();
        }
    }
}
