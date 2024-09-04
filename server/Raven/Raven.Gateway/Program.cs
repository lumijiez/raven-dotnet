using Ocelot.DependencyInjection;
using Raven.Gateway;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
var config = builder.Configuration;
var services = builder.Services;

config.SetBasePath(env.ContentRootPath)
    .AddJsonFile(Path.Combine("OcelotRoutes", "ocelot.json"))
    .AddEnvironmentVariables();

var startup = new Startup(config);

startup.ConfigureServices(services);

var app = builder.Build();

Startup.Configure(app, env);

app.Run();