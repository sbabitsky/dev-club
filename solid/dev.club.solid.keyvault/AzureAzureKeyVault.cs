using System;
using System.Threading.Tasks;

namespace dev.club.solid.azurekeyvault
{
    public class AzureAzureKeyVault : IAzureKeyVault
    {
        public Task<IAzureKeyVaultClient> CreateClientAsync()
        {
            throw new NotImplementedException();
        }
    }
}
