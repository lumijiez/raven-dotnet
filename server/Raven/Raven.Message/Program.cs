using Raven.Message;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
var config = builder.Configuration;
var services = builder.Services;

var startup = new Startup(config);

startup.ConfigureServices(services);

var app = builder.Build();

Startup.Configure(app, env);

app.Run();