using System;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault.Abstractions;

namespace Microsoft.Azure.KeyVault
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
