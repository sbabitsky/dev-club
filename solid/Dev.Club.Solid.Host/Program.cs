// See https://aka.ms/new-console-template for more information

using System.Runtime.Caching;
using System.Security.Claims;
using CertificationAuthority.Domain;
using Dev.Club.CertificationAuthority;
using Dev.Club.CertificationAuthority.Data.EF;
using Dev.Club.Solid.Core;
using Dev.Club.Solid.Core.Decorators;
using Dev.Club.Solid.Host;
using dev.club.solid.wcf;
using KeyVault.Abstractions;
using KeyVault.Microsoft.AzureKeyVault;
using Microsoft.Extensions.DependencyInjection;using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Host = Microsoft.Extensions.Hosting.Host;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Seq("http://localhost:5341")
    .CreateBootstrapLogger();

var host = Host.CreateDefaultBuilder() // Initialising the Host 
    .ConfigureServices((context, services) => { // Adding the DI container for configuration
        services.AddLogging(x => x.AddSerilog(Log.Logger));
    })
    .UseSerilog() // Add Serilog
    .Build(); // Build the Host


var logger = host.Services.GetRequiredService<ILogger<KeyVaultCertificateValidator>>();

logger.LogInformation("We done something.");

// DI container
// Register<IKeyVault>.As<AzureKeyVault>().DecoratedWith(AzureKeyVaultWithOptionalCache);

//IAzureKeyVault azureKeyVault = new AzureKeyVault();

IKeyVault keyVault = new AzureKeyVault(new Microsoft.Azure.KeyVault.AzureKeyVault());

bool isCacheEnabled = true;
if (isCacheEnabled)
{
    keyVault = new KeyVaultCacheDecorator(keyVault);
}

ICertificateProvider certificateProviderWithCache = new KeyVaultClientCacheDecorator(keyVault, MemoryCache.Default);

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
                Thumbprint = "123123213123",
                UniqueId = "Unique2"
            }
        }
    });


//--------------------
var cert = DefaultCertificateBuilder.Create("cn=foo", DateTimeOffset.Now, DateTimeOffset.Now.AddDays(5))
    .Validate()
    .Build();



try
{
    var validator = new KeyVaultCertificateValidator(externalCertificatesStore, logger);
    validator.Validate(certificate: cert);
}
finally
{
    Log.CloseAndFlush();
}































var adminPanel = new AdminPanel(keyVault); // from DI


var certificatesRepository = new CertificatesRepository(new ClaimsIdentity(), new CertificationAuthorityDbContext());

var certificate = new Certificate
{
    Thumbprint = "182391839012389012AD123123123",
    CompanyAddress = "Company A"
};

certificate = await certificatesRepository.SaveAsync(certificate);

Console.WriteLine("Hello, World!");
