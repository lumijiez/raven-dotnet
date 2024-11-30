using Raven.Auth;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddFilter("Microsoft.AspNetCore.DataProtection", LogLevel.Error);
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning);

var env = builder.Environment;
var config = builder.Configuration;
var services = builder.Services;

var startup = new Startup(config);

startup.ConfigureServices(services);

var app = builder.Build();

Startup.Configure(app, env);

app.Run();