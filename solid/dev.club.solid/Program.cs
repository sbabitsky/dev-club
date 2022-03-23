// See https://aka.ms/new-console-template for more information

using dev.club.solid.core;
using dev.club.solid.host;
using dev.club.solid.wcf;
using keyvault.abstractions;
using keyvault.amazon.aws.kms;
using keyvault.microsoft.azurekeyvault;
using nuget.amazon.aws.kms;

// DI container
// Register<IAzureKeyVault>.As<AzureKeyVault>().DecoratedWith(AzureKeyVaultWithOptionalCache);

//IAzureKeyVault azureKeyVault = new AzureKeyVault();

var azureKeyVault = new AzureKeyVault(new nuget.microsoft.azurekeyvault.AzureKeyVault());

IKeyVault keyVault = new KeyVaultWithOptionalCache(azureKeyVault, true);

var externalCertificatesStore = new ExternalCertificatesStore(
    async () => await azureKeyVault.CreateClientAsync(), new ExchangeConfiguration
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
    });


//--------------------
var validator = new KeyVaultCertificateValidator(externalCertificatesStore, null!);
validator.Validate(certificate: null!);

var adminPanel = new AdminPanel(keyVault); // from DI


Console.WriteLine("Hello, World!");
