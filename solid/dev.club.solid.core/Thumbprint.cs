namespace dev.club.solid.core
{
    public class Thumbprint
    {
        private Thumbprint(string thumbprint)
        {
            _thumbprint = thumbprint.ToUpperInvariant();
        }

        private readonly string _thumbprint;

        public static implicit operator Thumbprint(string thumbprint)
        {
            return new Thumbprint(thumbprint);
        }
    }
}
