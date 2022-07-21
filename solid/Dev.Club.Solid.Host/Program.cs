// See https://aka.ms/new-console-template for more information

using System.Security.Claims;
using CertificationAuthority.Domain;
using Dev.Club.CertificationAuthority.Data.EF;
using Dev.Club.Solid.Core;
using Dev.Club.Solid.Core.Decorators;
using Dev.Club.Solid.Host;
using dev.club.solid.wcf;
using KeyVault.Abstractions;
using KeyVault.Microsoft.AzureKeyVault;

// DI container
// Register<IKeyVault>.As<AzureKeyVault>().DecoratedWith(AzureKeyVaultWithOptionalCache);

//IAzureKeyVault azureKeyVault = new AzureKeyVault();

IKeyVault keyVault = new AzureKeyVault(new Microsoft.Azure.KeyVault.AzureKeyVault());

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


var certificatesRepository = new CertificatesRepository(new ClaimsIdentity(), new CertificationAuthorityDbContext());

var certificate = new Certificate
{
    Thumbprint = "182391839012389012AD123123123",
    CompanyAddress = "Company A"
};

certificate = await certificatesRepository.SaveAsync(certificate);

Console.WriteLine("Hello, World!");
