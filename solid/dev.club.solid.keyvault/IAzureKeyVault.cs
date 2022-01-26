using System.Threading.Tasks;

namespace dev.club.solid.azurekeyvault
{
    public interface IAzureKeyVault
    {
        Task<IAzureKeyVaultClient> CreateClientAsync();
    }
}
