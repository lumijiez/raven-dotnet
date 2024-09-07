using Microsoft.EntityFrameworkCore;
using Raven.Auth.Application.Services;
using Raven.Auth.Domain.Interfaces;
using Raven.Auth.Domain.Services;
using Raven.Auth.Infrastructure;
using Raven.Auth.Infrastructure.Repositories;

namespace Raven.Auth;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        StartupUtils.EvolveMigrate(configuration);
        
        services.AddDbContext<AuthDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("AuthDatabaseConnection")));

        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<AuthService>();
        services.AddScoped<AuthAppService>();
        services.AddScoped<IPasswordHasher, PassHashService>();

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

        app.MapControllers();
    }
}