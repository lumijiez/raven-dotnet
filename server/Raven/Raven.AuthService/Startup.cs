using Microsoft.EntityFrameworkCore;
using Npgsql;
using Raven.Auth.Data;

namespace Raven.AuthService;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;
    
    public void ConfigureServices(IServiceCollection services)
    {
        EnsureDatabaseCreated();
        
        services.AddDbContext<AuthDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("AuthDatabaseConnection")));
        
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
    }
    
    private static void EnsureDatabaseCreated()
    {
        const string databaseName = "auth";

        using var connection = new NpgsqlConnection("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres");
        connection.Open();
        using var command = new NpgsqlCommand($"SELECT 1 FROM pg_database WHERE datname = '{databaseName}'", connection);
        var exists = command.ExecuteScalar() != null;
        if (exists)
        {
            return;
        }
        using var createDbCommand = new NpgsqlCommand($"CREATE DATABASE {databaseName}", connection);
        createDbCommand.ExecuteNonQuery();
    }

    private void EvolveMigrate()
    {
        try
        {
            var cnx = new NpgsqlConnection(Configuration.GetConnectionString("AuthDatabaseConnection"));
            var evolve = new Evolve.Evolve(cnx, msg => Console.WriteLine(msg))
            {
                Locations = new[] { "db/migrations" },
                IsEraseDisabled = true,
            };

            evolve.Migrate();
        }
        catch (Exception ex)
        {
            _logger.LogCritical("Database migration failed.", ex);
            throw;
        }
    }
}