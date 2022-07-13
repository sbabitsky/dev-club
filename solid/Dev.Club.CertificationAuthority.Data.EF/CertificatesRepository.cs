using System.Security.Principal;
using CertificationAuthority.Domain;
using Dev.Club.CertificationAuthority.Data.Abstractions;

namespace Dev.Club.CertificationAuthority.Data.EF
{
    public class CertificatesRepository:  IRepository<Certificate>
    {
        private readonly IIdentity _identity;

        public CertificatesRepository(IIdentity identity)
        {
            //identity = WHO IS THAT?
            // principal
            //principal = IS HE/SHE ALLOWED TO DO SOMETHING?
            _identity = identity;
        }

        public Task SaveAsync(Certificate entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedBy = _identity.Name;
            throw new NotImplementedException();
        }
    }
}