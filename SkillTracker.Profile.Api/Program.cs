using System.Configuration;
using MediatR;
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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Profile Service", Version ="V1"});
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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Profile Microservice V1");
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

//public static IServiceCollection ConfigureEventBus(this IServiceCollection services, IConfiguration configuration)
//{
//    services.AddMassTransit(config =>
//    {
//        config.UsingRabbitMq((ctx, cfg) =>
//        {
//            cfg.Host(configuration["EventBusSettings:HostAddress"], c =>
//            {
//                c.Username(configuration["EventBusSettings:username"]);
//                c.Password(configuration["EventBusSettings:password"]);
//            });
//        });
//    });
//    services.AddMassTransitHostedService();
//    return services;
//}
