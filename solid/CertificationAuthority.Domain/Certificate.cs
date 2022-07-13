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
    }
}