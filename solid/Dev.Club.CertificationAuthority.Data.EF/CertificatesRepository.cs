using System.Security.Principal;
using CertificationAuthority.Domain;
using Dev.Club.CertificationAuthority.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Dev.Club.CertificationAuthority.Data.EF
{
    public class CertificatesRepository:  IRepository<Certificate>
    {
        private readonly IIdentity _identity;
        private readonly CertificationAuthorityDbContext _dbContext;

        public CertificatesRepository(IIdentity identity, CertificationAuthorityDbContext dbContext)
        {
            _identity = identity;
            _dbContext = dbContext;
        }

        public async Task<Certificate> SaveAsync(Certificate entity)
        {
            EntityEntry<Certificate> entityEntry;

            var persistedEntity = await _dbContext.Certificates.FindAsync(entity.Id);

            if (persistedEntity is null)
            {
                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = _identity.Name!;

                entityEntry = await _dbContext.Certificates.AddAsync(entity);
            }
            else
            {
                entityEntry = _dbContext.Certificates.Update(entity);

                entity.UpdatedDate = DateTime.Now;
                entity.UpdatedBy = _identity.Name!;
            }
            
            await _dbContext.SaveChangesAsync();

            return entityEntry.Entity;
        }
    }
}