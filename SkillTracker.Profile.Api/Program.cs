using System.Configuration;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SkillTracker.Domain.Core.Bus;
using SkillTracker.Infrastructure.IoC;
using SkillTracker.Profile.Data.DbContext;
using SkillTracker.Profile.Domain.EventHandlers;
using SkillTracker.Profile.Domain.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddApiVersioning(config =>
    {
        config.DefaultApiVersion = new ApiVersion(1,0);
        config.AssumeDefaultVersionWhenUnspecified = true;
        config.ReportApiVersions = true;
        config.ApiVersionReader = new UrlSegmentApiVersionReader();
    });
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Profile Service", Version ="V1"});
    });

builder.Services.AddVersionedApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });
builder.Services.AddMediatR(typeof(Program));

RegisterServices(builder.Services, builder.Configuration);

var app = builder.Build();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SkillTracker Microservice V1");
       
    });
//}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

SubscribeToEventBus(app);


app.Run();



void SubscribeToEventBus(WebApplication webApplication)
{
    var eventBus = app.Services.GetRequiredService<IEventBus>();
    eventBus.Subscribe<AddedProfileEvent, AddedProfileEventHandler>();
    eventBus.Subscribe<UpdatedProfileEvent, UpdatedProfileEventHandler>();
    //eventBus.Subscribe<SearchProfileEvent, SearchProfileEventHandler>();
}


void RegisterServices(IServiceCollection services,IConfiguration configuration)
{
    //CosmosDB: Configuration
    ConfigureCosmosDb(services, configuration);

    DependencyContainer.RegisterCustomServices(services);
}

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

    //services.AddDbContext<ProfileDbContext>(options =>
    //{


    //    var dbOptions = options.UseCosmos(configuration["CosmosDB:EndpointUri"],
    //        @"C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==", "SkillTracker").Options;
    //    //Action<DbContextOptions> PopulateData = async (dbOptions) =>
    //    //{

    //    //    await ProfileDbContext.SeedInitalDataSync(dbOptions);

    //    //};
    //    //PopulateData(dbOptions);
    //});
}

