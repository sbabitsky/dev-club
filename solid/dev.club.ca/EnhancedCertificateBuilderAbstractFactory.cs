using dev.club.ca.abstractions;

namespace dev.club.ca
{
    public class EnhancedCertificateBuilderFactory : ICertificateBuilderAbstractFactory
    {
        /// <summary>
        /// MOST LIKELY WILL CREATE a new one EACH TIME
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ICertificateBuilder Create()
        {
            return new EnhancedCertificateBuilder();
        }
    }
}
