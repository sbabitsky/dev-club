using System;
using System.Threading.Tasks;

namespace nuget.amazon.aws.kms
{
    public class AwsKeyManagementService : abstractions.IAwsKeyManagementService
    {
        public Task<abstractions.IAwsKeyManagementServiceClient> CreateClientAsync()
        {
            throw new NotImplementedException();
        }
    }
}
