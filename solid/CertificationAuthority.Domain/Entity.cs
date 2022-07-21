namespace CertificationAuthority.Domain
{
    public abstract class Entity<TId> : IAggregateRoot
    {
        public virtual TId Id { get; set; } // INT - in 95%, GUID, string

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public bool Active { get; set; } // Status, IsDeleted

        public virtual bool IsTransient()
        {
            return Equals(Id, default(TId));
        }
    }
}
