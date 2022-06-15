using System.Threading.Tasks;

namespace Microsoft.Azure.KeyVault.Abstractions
{
    public interface IAzureKeyVault
    {
        Task<IAzureKeyVaultClient> CreateClientAsync();
    }
}
