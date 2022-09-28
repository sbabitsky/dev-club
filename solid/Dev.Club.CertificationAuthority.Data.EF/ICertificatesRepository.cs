using CertificationAuthority.Domain;

namespace Dev.Club.CertificationAuthority.Data.EF;

public interface ICertificatesRepository
{
    ValueTask<Certificate> GetAsync(string id);

    ValueTask<Certificate?> FindAsync(string serialNumber);

    Task<Certificate> SaveAsync(Certificate certificate);
}