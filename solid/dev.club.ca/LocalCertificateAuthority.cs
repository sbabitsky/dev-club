﻿using dev.club.ca.abstractions;

namespace dev.club.ca
{
    // Role: Director
    public class LocalCertificateAuthority : ICertificateAuthority
    {
        private readonly ICertificateBuilderFactory _certificateBuilderFactory;


        public LocalCertificateAuthority(ICertificateBuilderFactory certificateBuilderFactory)
        {
            _certificateBuilderFactory = certificateBuilderFactory;
        }

        public IssuedCertificateResponse IssueSelfSignedCertificate(IssueSelfSignedCertificateRequest request)
        {
            var certificateBuilder = _certificateBuilderFactory.Create();

            certificateBuilder
                .Subject(request.Subject)
                .CertificateUsageType(CertificateUsageType.SelfSigned)
                .NotAfter(DateTimeOffset.Now.AddYears(1))
                .NotBefore(DateTimeOffset.Now);

            return new IssuedCertificateResponse
            {
                Certificate = certificateBuilder.Validate().Build()
            };
        }

        public IssuedCertificateResponse IssueSSLCertificate(IssueCertificateRequest request, CancellationToken cancellationToken)
        {
            var certificateBuilder = _certificateBuilderFactory.Create();

            certificateBuilder
                .Subject(request.Subject)
                .FriendlyName(request.FriendlyName)
                .CertificateUsageType(CertificateUsageType.SSL)
                .NotAfter(DateTimeOffset.Now.AddYears(1))
                .NotBefore(DateTimeOffset.Now);

            if (cancellationToken.IsCancellationRequested)
            {
                return new IssuedCertificateResponse();
            }

            return new IssuedCertificateResponse
            {
                Certificate = certificateBuilder.Validate().Build()
            };
        }
    }
}
