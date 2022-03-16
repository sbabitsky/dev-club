using System;
using System.Threading.Tasks;
using dev.club.solid.azurekeyvault.abstractions;
using nuget.microsoft.azurekeyvault.abstractions;

namespace nuget.microsoft.azurekeyvault
{
    public class AzureKeyVault : IAzureKeyVault
    {
        public Task<IAzureKeyVaultClient> CreateClientAsync()
        {
            // create a client
            // establish the connection
            // receive an access token
            throw new NotImplementedException();
        }
    }
}
