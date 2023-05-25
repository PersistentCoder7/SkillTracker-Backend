using SkillTracker.Search.Api.Extensions;
using SkillTracker.Search.Api.Infrastructure.ExceptionHandlers;
using SkillTracker.Search.Application.Interfaces;
using SkillTracker.Search.Application.Services;
using SkillTracker.Search.Cache;
using SkillTracker.Search.Cache.Interfaces;

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

        //Log the environment name
        logger.LogInformation($"SkillTracker.Search.Api Environment: {builder.Environment.EnvironmentName}");

        //Register a common global exception handler
        builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

        // Register Controllers.
        builder.Services.AddControllers();

        //Configure swagger version
        builder.AddSwaggerConfiguration();


        // Redis Cache
        builder.AddRedisCache(logger);

        //Register Micro-services commands and events
        builder.Services.RegisterMediatRCommandHandlers(logger);


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{
        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "SkillTracker Search Microservice V1"); });
        //}

        //This is essential to allow CORS policy to work.
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
