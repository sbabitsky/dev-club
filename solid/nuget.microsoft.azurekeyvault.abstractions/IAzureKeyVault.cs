using System.Threading.Tasks;
using nuget.microsoft.azurekeyvault.abstractions;

namespace dev.club.solid.azurekeyvault.abstractions
{
    public interface IAzureKeyVault
    {
        Task<IAzureKeyVaultClient> CreateClientAsync();
    }
}
