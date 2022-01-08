namespace dev.club.solid.Hidden
{
    internal class SecurityTokenValidationException : Exception
    {
        public SecurityTokenValidationException(string message) : base(message)
        {
            
        }

        public SecurityTokenValidationException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
