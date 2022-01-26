using System.Threading.Tasks;

namespace dev.club.solid.azurekeyvault.abstractions
{
    public interface IAzureKeyVault
    {
        Task<IAzureKeyVaultClient> CreateClientAsync();
    }
}
