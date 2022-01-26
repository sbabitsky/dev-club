using System;
using System.Threading.Tasks;
using dev.club.solid.azurekeyvault.abstractions;

namespace dev.club.solid.azurekeyvault
{
    public class AzureKeyVault : IAzureKeyVault
    {
        public Task<IAzureKeyVaultClient> CreateClientAsync()
        {
            throw new NotImplementedException();
        }
    }
}
