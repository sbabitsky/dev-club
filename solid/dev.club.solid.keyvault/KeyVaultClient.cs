﻿using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace dev.club.solid.keyvault
{
    public class KeyVaultClient : IKeyVaultClient
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<X509Certificate2> GetCertificateAsync(object any)
        {
            throw new NotImplementedException();
        }

        public Task<X509Certificate2> UploadCertificateAsync(object any)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCertificateAsync(object any)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetSecretAsync(object any)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSecretAsync(object any)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAnythingAsync(object any)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAnythingAsync(object any)
        {
            throw new NotImplementedException();
        }
    }
}