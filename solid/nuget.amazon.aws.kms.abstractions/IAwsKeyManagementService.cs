using System.Threading.Tasks;

namespace Amazon.Aws.Kms.Abstractions
{
    public interface IAwsKeyManagementService
    {
        Task<IAwsKeyManagementServiceClient> CreateClientAsync();
    }
}
