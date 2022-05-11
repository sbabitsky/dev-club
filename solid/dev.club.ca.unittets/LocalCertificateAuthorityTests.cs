using dev.club.ca.abstractions;
using Moq;
using NUnit.Framework;

namespace dev.club.ca.unittets
{
    public class LocalCertificateAuthorityTests
    {
        [SetUp]
        public void Setup()
        {
        }
        /*
        [Test]
        public void IssueSSLCertificate_Always_ReturnsACertificate()
        {
            var builder = new DefaultCertificateBuilder();

            var sut = new LocalCertificateAuthority(builder);

            var response = sut.IssueSSLCertificate(new IssueCertificateRequest
            {
                Subject = "cn=foo",
                FriendlyName = "Foo"
            });

            Assert.That(response.Certificate, Is.Not.Null);
        }

        [Test]
        public void IssueSSLCertificate_Always_ReturnsACertificateWithAFriendlyName()
        {
            var builder = new DefaultCertificateBuilder();

            var sut = new LocalCertificateAuthority(builder);

            var response = sut.IssueSSLCertificate(new IssueCertificateRequest
            {
                Subject = "cn=foo",
                FriendlyName = "Foo"
            });

            Assert.That(response.Certificate, Is.Not.Null);
        }
        */
    }
}