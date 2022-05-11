namespace dev.club.ca.abstractions
{
    public interface ICertificateBuilderFactory
    {
        ICertificateBuilder Create();

        ICertificateBuilder Get();
    }
}
