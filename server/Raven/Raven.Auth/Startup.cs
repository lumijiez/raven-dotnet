﻿using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Raven.Auth.Application.Services;
using Raven.Auth.Domain.Interfaces;
using Raven.Auth.Domain.Services;
using Raven.Auth.Infrastructure;
using Raven.Auth.Infrastructure.Repositories;

namespace Raven.Auth;

public class Startup(IConfiguration configuration)
{
    private readonly string _key = configuration["Jwt:Key"] ?? throw new KeyNotFoundException();
    private readonly string _issuer = configuration["Jwt:Issuer"] ?? throw new KeyNotFoundException();
    private readonly string _audience = configuration["Jwt:Audience"] ?? throw new KeyNotFoundException();
    private readonly int _expiresInMinutes = int.Parse(configuration["Jwt:ExpiresInMinutes"] ?? throw new KeyNotFoundException());
    private readonly int _refreshTokenExpiresInDays = int.Parse(configuration["Jwt:RefreshTokenExpiresInDays"] ?? throw new KeyNotFoundException());
    
    public void ConfigureServices(IServiceCollection services)
    {
        StartupUtils.EvolveMigrate(configuration);
        
        services.AddDbContext<AuthDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("AuthDatabaseConnection")));

        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<AuthService>();
        services.AddScoped<AuthAppService>();
        services.AddScoped<IPasswordHasher, PassHashService>();
        services.AddSingleton<JwtTokenHelper>();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _issuer,
                    ValidAudience = _audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key))
                };
            })
            .AddCookie()
            .AddGoogle(options =>
            {
                options.ClientId = configuration["Authentication:Google:ClientId"] ?? throw new InvalidOperationException();
                options.ClientSecret = configuration["Authentication:Google:ClientSecret"] ?? throw new InvalidOperationException();
            });;
        services.AddAuthorization();
            
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public static void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
}