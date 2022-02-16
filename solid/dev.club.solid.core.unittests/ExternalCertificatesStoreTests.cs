using System;
using System.Collections.Generic;
using System.Linq;
using dev.club.solid.azurekeyvault.abstractions;
using Moq;
using NUnit.Framework;

namespace dev.club.solid.core.unittests
{
    public class ExternalCertificatesStoreTests
    {
        private ExternalCertificatesStore _sut;
        
        [Test]
        public void IsTheCertificateIsStoredInAzure_WhenCertificateMappingsIsEmpty_ReturnsFalse()
        {
            var exchangeConfiguration = new ExchangeConfiguration
            {
                CertificateMappings = new List<CertificateMapping>()
            };

            const string thumbprint = "19D5D6E2860E4080AE5A6249BBA85809";

            _sut = new ExternalCertificatesStore(It.IsAny<IAzureKeyVault>(), exchangeConfiguration);
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

            _sut = new ExternalCertificatesStore(It.IsAny<IAzureKeyVault>(), exchangeConfiguration);
            Assert.That(_sut.IsTheCertificateIsStoredInAzure(thumbprint, out string uniqueId), Is.True);
            Assert.That(uniqueId, Is.EqualTo("SomeUniqueId"));
        }

        [Test]
        public void IsTheCertificateIsStoredInAzure_WhenCertificateMappingsHasTheThumbprintInItAndWeHaveALotOfCertificatesRegistered_ReturnsTrueAndTheUniqueId()
        {
            var exchangeConfiguration = new ExchangeConfiguration
            {
                CertificateMappings = new List<CertificateMapping>()
            };

            foreach (var i in Enumerable.Range(0, 1000000))
            {
                exchangeConfiguration.CertificateMappings.Add(new CertificateMapping
                {
                    Thumbprint = i.ToString(),
                    UniqueId = $"unique{i}"
                });
            }

            _sut = new ExternalCertificatesStore(It.IsAny<IAzureKeyVault>(), exchangeConfiguration);
            Assert.That(_sut.IsTheCertificateIsStoredInAzure("9999", out string uniqueId), Is.True);
            Assert.That(uniqueId, Is.EqualTo("unique9999"));
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

            _sut = new ExternalCertificatesStore(It.IsAny<IAzureKeyVault>(), exchangeConfiguration);
            Assert.That(_sut.IsTheCertificateIsStoredInAzure(thumbprint, out string uniqueId), Is.True);
            Assert.That(uniqueId, Is.EqualTo("SomeUniqueId"));
        }

        [Test]
        public void IsTheCertificateIsStoredInAzure_WhenCertificateTheConfigurationIsNull_ReturnsFalse()
        {
            const string thumbprint = "19d5d6E2860E4080AE5A6249BBa85809";

            _sut = new ExternalCertificatesStore(It.IsAny<IAzureKeyVault>(), null!);
            Assert.That(_sut.IsTheCertificateIsStoredInAzure(thumbprint, out string _), Is.False);
        }

        [Test]
        public void IsTheCertificateIsStoredInAzure_WhenCertificateMappingCollectionIsNull_ReturnsFalse()
        {
            const string thumbprint = "19d5d6E2860E4080AE5A6249BBa85809";

            var exchangeConfiguration = new ExchangeConfiguration();

            _sut = new ExternalCertificatesStore(It.IsAny<IAzureKeyVault>(), exchangeConfiguration);
            Assert.That(_sut.IsTheCertificateIsStoredInAzure(thumbprint, out string _), Is.False);
        }
    }
}