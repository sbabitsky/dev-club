using System;
using System.Threading.Tasks;
using Amazon.Aws.Kms.Abstractions;

namespace Amazon.Aws.Kms
{
    public class AwsKeyManagementService : IAwsKeyManagementService
    {
        public Task<IAwsKeyManagementServiceClient> CreateClientAsync()
        {
            throw new NotImplementedException();
        }
    }
}
