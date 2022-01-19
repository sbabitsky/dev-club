// See https://aka.ms/new-console-template for more information

using dev.club.solid.wcf;

var validator = new KeyVaultCertificateValidator(null!, null!);
validator.Validate(certificate: null!);
Console.WriteLine("Hello, World!");
