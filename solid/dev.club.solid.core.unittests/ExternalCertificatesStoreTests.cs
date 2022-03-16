using System;
using System.Collections.Generic;
using System.Linq;
using keyvault.abstractions;
using Moq;
using NUnit.Framework;

namespace dev.club.solid.core.unittests
{
    public class ExternalCertificatesStoreTests
    {
        private ExternalCertificatesStore _sut;
        private static readonly ExchangeConfiguration _hugeExchangeConfiguration;

        static ExternalCertificatesStoreTests()
        {
            _hugeExchangeConfiguration = new ExchangeConfiguration
            {
                CertificateMappings = new List<CertificateMapping>()
            };

            foreach (var i in Enumerable.Range(0, 1000))
            {
                _hugeExchangeConfiguration.CertificateMappings.Add(new CertificateMapping
                {
                    Thumbprint = i.ToString(),
                    UniqueId = $"unique{i}"
                });
            }
        }
        
        [Test]
        public void IsTheCertificateIsStoredInAzure_WhenCertificateMappingsIsEmpty_ReturnsFalse()
        {
            var exchangeConfiguration = new ExchangeConfiguration
            {
                CertificateMappings = new List<CertificateMapping>()
            };

            const string thumbprint = "19D5D6E2860E4080AE5A6249BBA85809";

            _sut = new ExternalCertificatesStore(It.IsAny<IKeyVault>(), exchangeConfiguration);
            Assert.That(_sut.IsTheCertificateIsStoredInAzure(thumbprint, out string _), Is.False);
        }

        [Test]
        public void IsTheCertificateIsStoredInAzure_WhenCertificateMappingsHasTheThumbprintInIt_ReturnsTrueAndTheUniqueId()
        {
            const string thumbprint = "19D5D6E2860E4080AE5A6249BBA85809";

            var exchangeConfiguration = new ExchangeConfiguration
            {
                CertificateMappings = new List<CertificateMapping>
                {
                    new CertificateMapping
                    {
                        Thumbprint = thumbprint,
                        UniqueId = "SomeUniqueId"
                    }
                }
            };

            _sut = new ExternalCertificatesStore(It.IsAny<IKeyVault>(), exchangeConfiguration);
            Assert.That(_sut.IsTheCertificateIsStoredInAzure(thumbprint, out string uniqueId), Is.True);
            Assert.That(uniqueId, Is.EqualTo("SomeUniqueId"));
        }

        [Test]
        public void IsTheCertificateIsStoredInAzure_WhenCertificateMappingsHasTheThumbprintInItAndWeHaveALotOfCertificatesRegistered_ReturnsTrueAndTheUniqueId()
        {
            for (int i = 0; i < 1 * 60 * 60; i++)
            {
                _sut = new ExternalCertificatesStore(It.IsAny<IKeyVault>(), _hugeExchangeConfiguration);
                Assert.That(_sut.IsTheCertificateIsStoredInAzure("999", out string uniqueId), Is.True);
                Assert.That(uniqueId, Is.EqualTo("unique999"));
            }
        }

        [Test]
        public void IsTheCertificateIsStoredInAzure_IsCaseInsensitive()
        {
            const string thumbprint = "19d5d6E2860E4080AE5A6249BBa85809";

            var exchangeConfiguration = new ExchangeConfiguration
            {
                CertificateMappings = new List<CertificateMapping>
                {
                    new CertificateMapping
                    {
                        Thumbprint = thumbprint,
                        UniqueId = "SomeUniqueId"
                    }
                }
            };

            _sut = new ExternalCertificatesStore(It.IsAny<IKeyVault>(), exchangeConfiguration);
            Assert.That(_sut.IsTheCertificateIsStoredInAzure(thumbprint, out string uniqueId), Is.True);
            Assert.That(uniqueId, Is.EqualTo("SomeUniqueId"));
        }

        [Test]
        public void IsTheCertificateIsStoredInAzure_WhenCertificateTheConfigurationIsNull_ThrownsAnException()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                _sut = new ExternalCertificatesStore(It.IsAny<IKeyVault>(), null!);
            });
        }

        [Test]
        public void IsTheCertificateIsStoredInAzure_WhenCertificateMappingCollectionIsNull_ThrownsAnException()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                _sut = new ExternalCertificatesStore(It.IsAny<IKeyVault>(), new ExchangeConfiguration());
            });
        }

        [Test]
        public void IsTheCertificateIsStoredInAzure_WhenCertificateMappingCollectionContainsMoreThanOneThumbprint_ThrowsTheException()
        {
            const string thumbprint = "19d5d6E2860E4080AE5A6249BBa85809";

            var exchangeConfiguration = new ExchangeConfiguration
            {
                CertificateMappings = new List<CertificateMapping>
                {
                    new CertificateMapping
                    {
                        Thumbprint = thumbprint,
                        UniqueId = "SomeUniqueId"
                    },
                    new CertificateMapping
                    {
                        Thumbprint = thumbprint,
                        UniqueId = "SomeUniqueId2"
                    }
                }
            };

            Assert.Throws<InvalidOperationException>(() =>
            {
                _sut = new ExternalCertificatesStore(It.IsAny<IKeyVault>(), exchangeConfiguration);
            });
        }
    }
}