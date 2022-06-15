namespace Dev.Club.Sandbox
{
    // singleton
    /*
     * Singleton is a creational design pattern that lets you ensure
that a class has only one instance, while providing a global
access point to this instance.
     */
    public class LicenseProvider
    {
        private LicenseProvider()
        {
            Console.WriteLine("ExternalCertificatesStoreProvider CREATED.");
        }

        private static readonly Lazy<LicenseProvider> _provider = new(() => new LicenseProvider());

        public static LicenseProvider Instance => _provider.Value;

        public void DoSomething()
        {
            Console.WriteLine("Did something.");
        }

        public bool IsActive { get; }

        public int NumberOfInstances { get; }
    }
}