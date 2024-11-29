using System.Security.Claims;
using Raven.Message.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Raven.Message.Application.Handlers;
using Raven.Message.Application.Services;
using Raven.Message.Infrastructure;
using Raven.Message.SignalR;

namespace Raven.Message;

public class Startup(IConfiguration configuration)
{
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<MongoDBSettings>(
            configuration.GetSection(nameof(MongoDBSettings)));
        
        services.AddSingleton<MongoContext>();

        services.AddScoped<MessageHandler>();
        services.AddScoped<ChatHandler>();
        services.AddScoped<UserChatHandler>();
        services.AddScoped<UserHandler>();
        
        services.AddScoped<ChatService>();
        services.AddScoped<UserService>();
        
        services.AddAuthentication()
            .AddJwtBearer(options =>
            {
                options.Authority = "https://identity:5001";
                options.TokenValidationParameters.ValidateAudience = false;
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            path.StartsWithSegments("/chat"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorizationBuilder()
            .AddPolicy("ApiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "message");
            });

        services.AddSignalR();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public static void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials());

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapHub<ChatHub>("/chat");

        app.MapGet("identity", (ClaimsPrincipal user) =>
            user.Claims.Select(c => new { c.Type, c.Value })).RequireAuthorization("ApiScope");

        app.MapControllers();

        app.Run();
    }
}