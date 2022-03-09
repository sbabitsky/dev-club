// See https://aka.ms/new-console-template for more information

using dev.club.solid.core;
using dev.club.solid.host;
using dev.club.solid.wcf;
using keyvault.abstractions;
using keyvault.microsoft.azurekeyvault;

// DI container
// Register<IAzureKeyVault>.As<AzureKeyVault>().DecoratedWith(AzureKeyVaultWithOptionalCache);

//IAzureKeyVault azureKeyVault = new AzureKeyVault();
IKeyVault keyVault = new KeyVaultWithOptionalCache(new AzureKeyVault(), true);

//--------------------
var validator = new KeyVaultCertificateValidator(new ExternalCertificatesStore(keyVault, new ExchangeConfiguration
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



var adminPanel = new AdminPanel(keyVault); // from DI


Console.WriteLine("Hello, World!");
