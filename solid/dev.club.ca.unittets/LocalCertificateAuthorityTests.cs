using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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

        [Test]
        public void InvalidCase()
        {
            var sut = new LocalCertificateAuthority(new EnhancedCertificateBuilderFactory());

            var tasks = new List<Task>(1000);
            for (int i = 0; i < 10; i++)
            {
                var task = Task.Run(() =>
                {
                    var issuedCertificateResponse = sut.IssueSSLCertificate(new IssueCertificateRequest
                    {
                        Subject = "CN=ssl",
                        FriendlyName = "ssl"
                    }, CancellationToken.None);

                    Assert.That(issuedCertificateResponse.Certificate, Is.Not.Null);
                    Assert.That(issuedCertificateResponse.Certificate.SubjectName.Name, Is.EqualTo("CN=ssl"));
                    Assert.That(issuedCertificateResponse.Certificate.FriendlyName, Is.EqualTo("ssl"));

                    var selfSignedCertificateResponse = sut.IssueSelfSignedCertificate(new IssueSelfSignedCertificateRequest
                    {
                        Subject = "CN=self-signed"
                    });

                    Assert.That(selfSignedCertificateResponse.Certificate, Is.Not.Null);
                    Assert.That(selfSignedCertificateResponse.Certificate.SubjectName.Name, Is.EqualTo("CN=self-signed"));
                    Assert.That(selfSignedCertificateResponse.Certificate.FriendlyName, Is.Null.Or.Empty);
                });

                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());
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