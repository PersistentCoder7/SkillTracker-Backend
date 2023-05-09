using SkillTracker.Profile.Api.Extensions;
using SkillTracker.Profile.Api.Infrastructure.ExceptionHandlers;

var builder = WebApplication.CreateBuilder(args);
// Add configuration sources. 
builder.Configuration
    .AddEnvironmentVariables("ST_") 
    .AddJsonFile("appsettings.json");

// Register Controllers.
builder.Services.AddControllers();

//Configure swagger version
builder.AddSwaggerConfiguration();

//Register Micro-services commands and events
builder.Services.RegisterMicroServices();

//CosmosDB: Configuration
builder.AddCosmosDb();

//Register a common global exception handler
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
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

//Add the subscribers to listen to events
app.EnListSubscribeToEventBus();
app.Run();

