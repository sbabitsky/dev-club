using System.Threading.Tasks;

namespace dev.club.solid.keyvault
{
    public interface IKeyVault
    {
        Task<IKeyVaultClient> CreateClientAsync();
    }
}
