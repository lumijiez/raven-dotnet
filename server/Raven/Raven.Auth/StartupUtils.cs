using System.Reflection;
using EvolveDb;
using Npgsql;

namespace Raven.Auth;

public static class StartupUtils
{
    public static void EvolveMigrate(IConfiguration config)
    {
        EnsureDatabaseCreated();
        
        var assembly = Assembly.Load("Raven.Auth.Infrastructure"); 
        try
        {
            var cnx = new NpgsqlConnection(config.GetConnectionString("AuthDatabaseConnection"));
            var evolve = new Evolve(cnx, Console.WriteLine)
            {
                EmbeddedResourceAssemblies = [assembly],
                EmbeddedResourceFilters = new[] { "Raven.Auth.Infrastructure.Migrations" },
                IsEraseDisabled = true,
            };

            evolve.Migrate();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Database migration failed.");
            throw;
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
}