namespace CertificationAuthority.Domain
{
    public class Certificate : Entity<string>
    {
        public override string Id
        {
            get => Thumbprint;

            set => Thumbprint = value;
        }

        public string Thumbprint { get; set; } // PK, Clustered Index

        public string Subject { get; set; }

        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }

        public DateTime ExpirationDate { get; set; }

        public State State { get; set; }

        public override bool IsTransient()
        {
            throw new InvalidOperationException("Foo=Bar");
        }
    }

    public enum State
    {
        None = 0,
        Active = 1, 
        Inactive = 2
    }

    public enum State
    {
        Active = 0,
        Inactive = 1,

    }
}