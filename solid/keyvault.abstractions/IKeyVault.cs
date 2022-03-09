using System.Threading.Tasks;

namespace keyvault.abstractions
{
    public interface IKeyVault
    {
        Task<IKeyVaultClient> CreateClientAsync();
    }
}
