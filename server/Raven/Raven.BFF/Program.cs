using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Raven.BFF.Application.Services;
using Raven.BFF.Domain.Settings;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddScoped(_ =>
{
    var settings = new OpenIDSettings();
    config.GetSection("OpenIdConnectSettings").Bind(settings);
    return settings;
});

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("/app/data-protection-keys"))
    .ProtectKeysWithCertificate(LoadCertificate())
    .UseCryptographicAlgorithms(
        new AuthenticatedEncryptorConfiguration
        {
            EncryptionAlgorithm = EncryptionAlgorithm.AES_192_CBC,
            ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
        });

static X509Certificate2 LoadCertificate()
{
    const string certPath = "/app/data-protection-keys/data-protection-cert.pfx";
    
    if (File.Exists(certPath))
    {
        return new X509Certificate2(
            certPath, 
            "DataProtectionPassword", 
            X509KeyStorageFlags.Exportable
        );
    }

    using var algorithm = RSA.Create(keySizeInBits: 2048);
    var subject = new X500DistinguishedName("CN=DataProtectionKeyCertificate");
    var request = new CertificateRequest(
        subject, 
        algorithm, 
        HashAlgorithmName.SHA256, 
        RSASignaturePadding.Pkcs1
    );
    request.CertificateExtensions.Add(
        new X509BasicConstraintsExtension(false, false, 0, false)
    );

    var certificate = request.CreateSelfSigned(
        DateTimeOffset.UtcNow, 
        DateTimeOffset.UtcNow.AddYears(10)
    );

    var certBytes = certificate.Export(
        X509ContentType.Pfx, 
        "DataProtectionPassword"
    );

    Directory.CreateDirectory(Path.GetDirectoryName(certPath) ?? string.Empty);
    File.WriteAllBytes(certPath, certBytes);

    return new X509Certificate2(
        certPath, 
        "DataProtectionPassword", 
        X509KeyStorageFlags.Exportable
    );
}

builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.MapControllers();

app.Run();