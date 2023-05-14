using MassTransit;
using SkillTracker.Profile.Api.Extensions;
using SkillTracker.Profile.Api.Infrastructure.ExceptionHandlers;
using SkillTracker.Profile.Api.Utils;

var builder = WebApplication.CreateBuilder(args);
// Add configuration sources. 
builder.Configuration
    .AddEnvironmentVariables("ST_") 
    .AddJsonFile("appsettings.json");

builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
    // Add other logging providers as needed
});

// Register Controllers.
builder.Services.AddControllers();

//Configure swagger version
builder.AddSwaggerConfiguration();

//Register Micro-services commands and events
builder.Services.RegisterMediatRCommandHandlers();

//CosmosDB: Configuration
builder.AddCosmosDb();

//Redis Cache
builder.AddRedisCache();

//Register a common global exception handler
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

//Add masstransit to allow messaging
builder.AddRabbitMQ();

var app = builder.Build();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "SkillTracker Microservice V1"); });
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

