using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dev.club.solid
{
    internal class AzureKeyVault : IKeyVault
    {
        public Task<IKeyVaultClient> CreateClientAsync()
        {
            throw new NotImplementedException();
        }
    }
}
