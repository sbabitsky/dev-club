// See https://aka.ms/new-console-template for more information

using dev.club.solid.azurekeyvault;
using dev.club.solid.azurekeyvault.abstractions;
using dev.club.solid.core;
using dev.club.solid.host;
using dev.club.solid.wcf;

// DI container
// Register<IAzureKeyVault>.As<AzureKeyVault>().DecoratedWith(AzureKeyVaultWithOptionalCache);

//IAzureKeyVault azureKeyVault = new AzureKeyVault();
IAzureKeyVault azureKeyVault = new AzureKeyVaultWithOptionalCache(new AzureKeyVault(), true);

//--------------------
var validator = new KeyVaultCertificateValidator(new ExternalCertificatesStore(azureKeyVault, new ExchangeConfiguration
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
}), null!);
validator.Validate(certificate: null!);



var adminPanel = new AdminPanel(azureKeyVault); // from DI


Console.WriteLine("Hello, World!");
