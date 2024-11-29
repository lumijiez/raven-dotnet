using Microsoft.AspNetCore.DataProtection;
using Raven.BFF.Application.Services;
using Raven.BFF.Domain.Settings;

namespace Raven.BFF;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped(_ =>
        {
            var settings = new OpenIDSettings();
            configuration.GetSection("OpenIdConnectSettings").Bind(settings);
            return settings;
        });

        services.AddScoped<AuthenticationService>();
        services.AddHttpClient();
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

        app.UseHttpsRedirection();

        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(_ => true)
            .AllowCredentials());

        app.MapControllers();
    }
}