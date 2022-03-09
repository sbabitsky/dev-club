using System.Threading.Tasks;

namespace nuget.amazon.aws.keyvault.abstractions
{
    public interface IAwsKeyVault
    {
        Task<IAwsKeyVaultClient> CreateClientAsync();
    }
}
