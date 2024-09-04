using System.Collections.Immutable;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Raven.Gateway;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddOcelot(Configuration);
    }

    public static void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        // Enable on deploy!
        // app.UseHttpsRedirection();
        app.UseOcelot().Wait();
    }
}
