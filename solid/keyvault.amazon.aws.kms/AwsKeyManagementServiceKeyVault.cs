using System;
using System.Threading.Tasks;
using KeyVault.Abstractions;

namespace KeyVault.Amazon.Aws.Kms
{
    public class AwsKeyManagementServiceKeyVault : IKeyVault
    {
        public Task<IKeyVaultClient> CreateClientAsync()
        {
            throw new NotImplementedException();
        }
    }
}
