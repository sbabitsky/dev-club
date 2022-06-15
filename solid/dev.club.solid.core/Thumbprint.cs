namespace Dev.Club.Solid.Core
{
    public sealed class Thumbprint
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

        public static implicit operator string(Thumbprint thumbprint)
        {
            return thumbprint._thumbprint;
        }

        public override bool Equals(object obj)
        {
            if (obj is Thumbprint b)
            {
                return string.Equals(_thumbprint, b._thumbprint);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return _thumbprint.GetHashCode();
        }
    }
}
