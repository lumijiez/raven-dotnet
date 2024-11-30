using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Raven.Auth.Data;
using Raven.Auth.Interfaces;
using Raven.Auth.Models;
using Raven.Auth.Services;

namespace Raven.Auth;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AuthDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            
        services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();
            
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddGoogle(options =>
            {
                options.ClientId = configuration["Google:ClientId"]!;
                options.ClientSecret = configuration["Google:ClientSecret"]!;
                options.CallbackPath = "/signin-google";

                options.Events.OnCreatingTicket = async context =>
                {
                    var claims = context.Principal?.Identities.FirstOrDefault()?.Claims;
                    var googleId = claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                    var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<AppUser>>();
                    var user = await userManager.FindByLoginAsync(GoogleDefaults.AuthenticationScheme, googleId!);

                    if (context.AccessToken != null)
                    {
                        context.Identity?.AddClaim(new Claim("urn:google:accesstoken", context.AccessToken));
                    }
                
                    if (context.Properties.Items.TryGetValue(".Token.id_token", out var idToken))
                    {
                        context.Identity?.AddClaim(new Claim("id_token", idToken!));
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
                                googleId!,
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
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateIssuerSigningKey = true,
                };
            });

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public static void Configure(WebApplication app, IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
            dbContext.Database.Migrate();
        }

        if (app.Environment.IsProduction())
        {
            app.UseHttpsRedirection();
        }
        
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
}