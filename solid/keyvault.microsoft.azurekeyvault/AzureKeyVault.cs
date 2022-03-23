using System;
using System.Threading.Tasks;
using dev.club.solid.azurekeyvault.abstractions;
using keyvault.abstractions;

namespace keyvault.microsoft.azurekeyvault
{
    public class AzureKeyVault : IKeyVault
    {
        private readonly IAzureKeyVault _azureKeyVault;

        public AzureKeyVault(IAzureKeyVault azureKeyVault)
        {
            _azureKeyVault = azureKeyVault;
        }
        public async Task<IKeyVaultClient> CreateClientAsync()
        {
            // authorization
            // request access token
            // 3600 => 6 min
            return new AzureKeyVaultClient(await _azureKeyVault.CreateClientAsync());
        }
    }
}
