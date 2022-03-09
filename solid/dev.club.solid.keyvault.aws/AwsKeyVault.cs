using System;
using System.Threading.Tasks;
using nuget.amazon.aws.keyvault.abstractions;

namespace nuget.amazon.aws.keyvault
{
    public class AwsKeyVault : IAwsKeyVault
    {
        public Task<IAwsKeyVaultClient> CreateClientAsync()
        {
            throw new NotImplementedException();
        }
    }
}
