using System.Collections.Generic;

namespace dev.club.solid.azurekeyvault
{
    public class ExchangeConfiguration
    {
        public IList<CertificateMapping> CertificateMappings { get; set; }
    }

    public class CertificateMapping
    {
        public string Thumbprint { get; set; }

        public string UniqueId { get; set; }
    }
}
