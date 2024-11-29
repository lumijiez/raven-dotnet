using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Raven.Message.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.IdentityModel.Tokens;
using Raven.Message.Application.Handlers;
using Raven.Message.Application.Services;
using Raven.Message.Infrastructure;
using Raven.Message.SignalR;

namespace Raven.Message;

public class Startup(IConfiguration configuration)
{
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<MongoDBSettings>(
            configuration.GetSection(nameof(MongoDBSettings)));
        
        services.AddSingleton<MongoContext>();

        services.AddScoped<MessageHandler>();
        services.AddScoped<ChatHandler>();
        services.AddScoped<UserChatHandler>();
        services.AddScoped<UserHandler>();
        
        services.AddScoped<ChatService>();
        services.AddScoped<UserService>();
        
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key not configured"))
                    ),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            path.StartsWithSegments("/chat"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddSignalR();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public static void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(_ => true)
            .AllowCredentials());

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapHub<ChatHub>("/chat");

        app.MapGet("identity", (ClaimsPrincipal user) =>
            user.Claims.Select(c => new { c.Type, c.Value })).RequireAuthorization("ApiScope");

        app.MapControllers();

        app.Run();
    }
}