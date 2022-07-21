namespace Dev.Club.CertificationAuthority.Data.Abstractions
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> SaveAsync(TEntity entity);
    }
}