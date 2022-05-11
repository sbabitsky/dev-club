using dev.club.ca.abstractions;

namespace dev.club.ca
{
    public class EnhancedCertificateBuilderFactory : ICertificateBuilderFactory
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

        /// <summary>
        /// Can CREATE a new one or CAN REUSE some existing instance
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ICertificateBuilder Get()
        {
            throw new NotImplementedException();
        }
    }
}
