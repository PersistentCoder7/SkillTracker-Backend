using Azure.Core;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using SkillTracker.Profile.Data.DbContext;

namespace SkillTracker.Profile.Api.Extensions;

public static class CosmosDBExtensions
{
    public static void AddCosmosDb(this WebApplicationBuilder builder)
    {

        var services = builder.Services;
        var configuration = builder.Configuration;
        ;

        var EndpointUri = configuration["CosmosDB:EndpointUri"];
        var Key = configuration["CosmosDB:Key"];
        var ConnectionString = configuration["CosmosDB:ConnectionString"];
        var Database = configuration["CosmosDB:Database"];
        var Container = configuration["CosmosDB:Container"];

        CosmosClientOptions cosmosClientOptions = new CosmosClientOptions()
        {
            ConnectionMode = ConnectionMode.Gateway,
            HttpClientFactory = () =>
            {
                var httpClientHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };
                return new HttpClient(httpClientHandler);
            }
        }; //This should be of dev mode only.
        CosmosClient cosmosClient = new CosmosClient(EndpointUri, Key, cosmosClientOptions);
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
}