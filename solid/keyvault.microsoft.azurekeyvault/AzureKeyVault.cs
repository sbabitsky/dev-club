using System.Threading.Tasks;
using KeyVault.Abstractions;
using Microsoft.Azure.KeyVault.Abstractions;

namespace KeyVault.Microsoft.AzureKeyVault
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
