using System;
using System.Threading.Tasks;
using keyvault.abstractions;

namespace keyvault.amazon.aws.kms
{
    public class AwsKeyManagementServiceKeyVault : IKeyVault
    {
        public Task<IKeyVaultClient> CreateClientAsync()
        {
            throw new NotImplementedException();
        }
    }
}
