using Azure.Core;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using SkillTracker.Profile.Data.DbContext;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;

namespace SkillTracker.Profile.Api.Extensions;

public static class CosmosDBExtensions
{
    public static void AddCosmosDb(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var connectionString = configuration["CosmosDB:ConnectionString"];
        var databaseName = configuration["CosmosDB:Database"];
        var containerName = configuration["CosmosDB:Container"];
        var hostName = Environment.GetEnvironmentVariable("COSMOSDB_HOST") ?? "192.168.0.15";
        connectionString = connectionString.Replace("192.168.0.15", hostName);

        services.AddHttpClient();

        services.AddSingleton<CosmosClient>(sp =>
        {
            CosmosClientOptions cosmosClientOptions = new CosmosClientOptions()
            {
                HttpClientFactory = () =>
                {
                    HttpMessageHandler httpMessageHandler = new HttpClientHandler()
                    {
                        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    };

                    return new HttpClient(httpMessageHandler);
                },
                ConnectionMode = ConnectionMode.Gateway
            };

            return new CosmosClient(connectionString, cosmosClientOptions);
        });

       
            var cosmosClient = services.BuildServiceProvider().GetRequiredService<CosmosClient>();
            var database = cosmosClient.CreateDatabaseIfNotExistsAsync(databaseName).GetAwaiter().GetResult();
            database.Database.CreateContainerIfNotExistsAsync(containerName, "/associateId").GetAwaiter().GetResult();
        

        services.AddDbContext<ProfileDbContext>(options =>
        {
            options.UseCosmos(connectionString, databaseName);
        });
    }

}