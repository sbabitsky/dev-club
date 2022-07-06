namespace CertificationAuthority.Domain
{
    public class Certificate
    {
        public string Thumbprint { get; set; } // PK, Clustered Index 

        public string Subject { get; set; }

        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}