using System.Threading.Tasks;

namespace KeyVault.Abstractions
{
    public interface IKeyVault
    {
        Task<IKeyVaultClient> CreateClientAsync();
    }
}
