// See https://aka.ms/new-console-template for more information

using dev.club.sandbox;
using dev.club.solid.core;
using dev.club.solid.core.decorators;
using dev.club.solid.host;
using dev.club.solid.wcf;
using keyvault.abstractions;
using keyvault.microsoft.azurekeyvault;

// DI container
// Register<IKeyVault>.As<AzureKeyVault>().DecoratedWith(AzureKeyVaultWithOptionalCache);

//IAzureKeyVault azureKeyVault = new AzureKeyVault();

IKeyVault keyVault = new AzureKeyVault(new nuget.microsoft.azurekeyvault.AzureKeyVault());

bool isCacheEnabled = true;
if (isCacheEnabled)
{
    keyVault = new KeyVaultCacheDecorator(keyVault);
}

ICertificateProvider certificateProviderWithCache = new KeyVaultClientCacheDecorator(keyVault);

var externalCertificatesStore = new ExternalCertificatesStore(certificateProviderWithCache, new ExchangeConfiguration
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
