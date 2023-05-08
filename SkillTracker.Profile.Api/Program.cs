using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using SkillTracker.Profile.Api.Extensions;
using SkillTracker.Profile.Api.Infrastructure.ExceptionHandlers;
using SkillTracker.Profile.Data.DbContext;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();

//Configure swagger versioning
builder.AddSwaggerConfiguration();

//Register all the Commands and events for Microservices
builder.Services.RegisterMicroServices();

//CosmosDB: Configuration
builder.AddCosmosDb();

//Register global exception handler
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


void ConfigureCosmosDb(IServiceCollection services, IConfiguration configuration)
{
    var EndpointUri = configuration["CosmosDB:EndpointUri"];
    var Key = configuration["CosmosDB:Key"];
    var ConnectionString = configuration["CosmosDB:ConnectionString"];
    var Database = configuration["CosmosDB:Database"];
    var Container = configuration["CosmosDB:Container"];

    CosmosClient cosmosClient = new CosmosClient(EndpointUri, Key);
    services.AddSingleton<CosmosClient>(cosmosClient);
    Database database = cosmosClient.CreateDatabaseIfNotExistsAsync(Database).Result;
    database.CreateContainerIfNotExistsAsync(Container, "/associateId");
    services.AddDbContext<ProfileDbContext>(option => option.UseCosmos(ConnectionString, Database));

}