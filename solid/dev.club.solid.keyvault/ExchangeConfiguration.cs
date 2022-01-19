using System.Collections.Generic;

namespace dev.club.solid.keyvault
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
