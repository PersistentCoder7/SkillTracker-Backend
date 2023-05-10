using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using SkillTracker.Profile.Api.Utils;
using SkillTracker.Profile.Data.DbContext;

namespace SkillTracker.Profile.Api.Extensions;

public static class CosmosDBExtensions
{
    public static void AddCosmosDb(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        var hostName = Environment.GetEnvironmentVariable("CosmosDbHost");
        var connectionString = configuration["CosmosDbConnectionString"].Replace("<host>", hostName);

        // Parse the Cosmos DB connection string using CosmosConnectionStringBuilder.
        var parser = new STCosmosConnectionStringParser();
        parser.ParseConnectionString(connectionString);

        // Use the parsed values from the Cosmos DB connection string.
        var accountEndpoint = parser.AccountEndpoint;
        var accountKey = parser.AccountKey;
        var database = parser.Database;
        var containerName = parser.Container;

        // Create a custom HttpClientHandler that disables SSL verification.
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

        services.AddSingleton<CosmosClient>(sp =>
        {
            CosmosClientOptions cosmosClientOptions = new CosmosClientOptions()
            {
                HttpClientFactory = () => new HttpClient(handler)
            };
            return new CosmosClient(connectionString, cosmosClientOptions);
        });

        var cosmosClient = services.BuildServiceProvider().GetRequiredService<CosmosClient>();
        var db = cosmosClient.CreateDatabaseIfNotExistsAsync(database).GetAwaiter().GetResult();
        db.Database.CreateContainerIfNotExistsAsync(containerName, "/associateId").GetAwaiter().GetResult();

        services.AddDbContext<ProfileDbContext>(options =>
        {
            options.UseCosmos(connectionString, database);
        });
    }
}