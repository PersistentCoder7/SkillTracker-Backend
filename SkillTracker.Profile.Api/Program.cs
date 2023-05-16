using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using SkillTracker.Profile.Api.Extensions;
using SkillTracker.Profile.Api.Infrastructure.ExceptionHandlers;
using SkillTracker.Profile.Api.Utils;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add configuration sources.
        builder.TweakConfiguration();

        // Logging Initialization
        builder.Logging.AddConsole();
        var logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();

        // Register Controllers.
        builder.Services.AddControllers();

        // Configure swagger version
        builder.AddSwaggerConfiguration();

        // Register Micro-services commands and events
        builder.Services.RegisterMediatRCommandHandlers(logger);

        // CosmosDB: Configuration
        builder.AddCosmosDb(logger);

        // Redis Cache
        builder.AddRedisCache(logger);

        // Register a common global exception handler
        builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

        // Add masstransit to allow messaging
        builder.AddRabbitMQ(logger);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        // if (app.Environment.IsDevelopment())
        // {
        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "SkillTracker Microservice V1"); });
        // }

        // This is essential to allow CORS policy to work.
        app.UseCors(builder =>
        {
            builder
                .SetIsOriginAllowed((host) => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        app.MapControllers();

        app.Run();
    }
}
