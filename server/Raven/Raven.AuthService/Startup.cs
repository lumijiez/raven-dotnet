﻿using System.Reflection;
using EvolveDb;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Raven.Auth.Data;
using static System.Console;

namespace Raven.AuthService;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;
    
    public void ConfigureServices(IServiceCollection services)
    {
        EnsureDatabaseCreated();
        EvolveMigrate();
        
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
        var assembly = Assembly.Load("Raven.Auth.Data"); 
        try
        {
            var cnx = new NpgsqlConnection(Configuration.GetConnectionString("AuthDatabaseConnection"));
            var evolve = new Evolve(cnx, WriteLine)
            {
                EmbeddedResourceAssemblies = [assembly],
                EmbeddedResourceFilters = new[] { "Raven.Auth.Data.Migrations" },
                IsEraseDisabled = true,
            };

            evolve.Migrate();
        }
        catch (Exception ex)
        {
            WriteLine("Database migration failed.");
            throw;
        }
    }
}