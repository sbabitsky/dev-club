using System;

namespace dev.club.solid.wcf
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
