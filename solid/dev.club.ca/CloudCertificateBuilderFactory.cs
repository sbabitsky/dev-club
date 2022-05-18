using dev.club.ca.abstractions;

namespace dev.club.ca
{
    public class Host
    {
        public static void Main()
        {
            // load configuration
            var defaultConfiguration = new CloudEndpointConfiguration
            {
                Endpoint = "1",
                ApiKey = "secret"
            };

            // DI container . register (cloudEC)

            

            CloudCertificateBuilderFactory bf = new CloudCertificateBuilderFactory((CloudEndpointConfiguration)defaultConfiguration.Clone());
            bf.Create();

            SomeClient sc = new SomeClient((CloudEndpointConfiguration)defaultConfiguration.Clone());

            bf.Create();
        }
    }
    public class SomeClient
    {
        public SomeClient(CloudEndpointConfiguration cloudEndpointConfiguration)
        {
            cloudEndpointConfiguration.UniqueClientId = nameof(SomeClient);
        }
    }
    internal class CloudCertificateBuilderFactory : ICertificateBuilderFactory
    {
        private CloudCertificateBuilder _prototypeBuilder;
        private string _accessToken;

        public CloudCertificateBuilderFactory(CloudEndpointConfiguration cloudEndpointConfiguration)
        {

            cloudEndpointConfiguration.UniqueClientId = nameof(CloudCertificateBuilderFactory);
            // call some API (endpoint + apiKey)
            //Thread.Sleep(5000);
            _accessToken = Guid.NewGuid().ToString();
            _prototypeBuilder = new CloudCertificateBuilder(_accessToken);
        }

        public ICertificateBuilder Create()
        {
            return (ICertificateBuilder)_prototypeBuilder.Clone();
        }
    }

    public class CloudEndpointConfiguration : ICloneable
    {
        public string Endpoint { get; set; }

        public string ApiKey { get; set; }

        public string UniqueClientId { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
