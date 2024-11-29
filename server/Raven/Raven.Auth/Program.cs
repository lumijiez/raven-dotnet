using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.IdentityModel.Tokens;
using Raven.Auth.Data;
using Raven.Auth.Interfaces;
using Raven.Auth.Models;
using Raven.Auth.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();
    
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    })
    .AddCookie()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Google:ClientId"]!;
        options.ClientSecret = builder.Configuration["Google:ClientSecret"]!;
        options.CallbackPath = "/signin-google";

        options.Events.OnCreatingTicket = async context =>
        {
            var claims = context.Principal?.Identities.FirstOrDefault()?.Claims;
            var googleId = claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<AppUser>>();
            var user = await userManager.FindByLoginAsync(GoogleDefaults.AuthenticationScheme, googleId);

            if (context.AccessToken != null)
            {
                context.Identity?.AddClaim(new Claim("urn:google:accesstoken", context.AccessToken));
            }
        
            if (context.Properties.Items.TryGetValue(".Token.id_token", out var idToken))
            {
                context.Identity?.AddClaim(new Claim("id_token", idToken));
            }
            
            if (user == null)
            {
                user = new AppUser
                {
                    UserName = context.Principal?.FindFirstValue(ClaimTypes.Email),
                    Email = context.Principal?.FindFirstValue(ClaimTypes.Email),
                    GoogleId = googleId,
                    IsExternalAccountLinked = true
                };

                var createResult = await userManager.CreateAsync(user);
                if (createResult.Succeeded)
                {
                    await userManager.AddLoginAsync(user, new UserLoginInfo(
                        GoogleDefaults.AuthenticationScheme,
                        googleId,
                        "Google"
                    ));
                }
            }
        };
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateIssuerSigningKey = true,
        };
    });
    
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();

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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();