using CertificationAuthority.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dev.Club.CertificationAuthority.Data.EF
{
    public class CertificationAuthorityDbContext : DbContext
    {
        public DbSet<Certificate> Certificates { get; set; }
    }
}
