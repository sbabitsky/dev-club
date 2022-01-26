// See https://aka.ms/new-console-template for more information

using dev.club.solid.azurekeyvault;
using dev.club.solid.wcf;

var validator = new KeyVaultCertificateValidator(null!, new ExchangeConfiguration
{
    CertificateMappings = new List<CertificateMapping>
    {
        new CertificateMapping
        {
            Thumbprint = "123123213",
            UniqueId = "12332UniqueId"
        },
        new CertificateMapping
        {
            Thumbprint = "123123213",
            UniqueId = "Unique2"
        }
    }
}, null!);
validator.Validate(certificate: null!);
Console.WriteLine("Hello, World!");
