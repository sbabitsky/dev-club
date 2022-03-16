using System.Threading.Tasks;

namespace nuget.amazon.aws.kms.abstractions
{
    public interface IAwsKeyManagementService
    {
        Task<IAwsKeyManagementServiceClient> CreateClientAsync();
    }
}
