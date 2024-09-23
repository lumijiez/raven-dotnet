using System.Security.Claims;
using MessageService.Application;
using MessageService.Application.Services;
using MessageService.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MessageService;

public class Startup(IConfiguration configuration)
{
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<MongoDBSettings>(
            configuration.GetSection(nameof(MongoDBSettings)));
        
        services.AddSingleton<MongoContext>();

        services.AddSingleton<MessageHandler>();
        services.AddSingleton<ChatHandler>();
        services.AddSingleton<UserChatHandler>();
        
        services.AddAuthentication()
            .AddJwtBearer(options =>
            {
                options.Authority = "https://localhost:5001";
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