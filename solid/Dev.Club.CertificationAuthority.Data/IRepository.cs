namespace Dev.Club.CertificationAuthority.Data.Abstractions
{
    public interface IRepository<TEntity>
    {
        Task SaveAsync(TEntity entity);
    }
}