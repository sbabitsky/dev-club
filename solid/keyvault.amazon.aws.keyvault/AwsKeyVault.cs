﻿using System;
using System.Threading.Tasks;
using keyvault.abstractions;

namespace keyvault.amazon.aws.keyvault
{
    public class AwsKeyVault : IKeyVault
    {
        public Task<IKeyVaultClient> CreateClientAsync()
        {
            throw new NotImplementedException();
        }
    }
}