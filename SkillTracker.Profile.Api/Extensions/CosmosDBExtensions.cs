using Azure.Core;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using SkillTracker.Profile.Data.DbContext;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Cosmos.Fluent;

namespace SkillTracker.Profile.Api.Extensions;

public static class CosmosDBExtensions
{
    public static void AddCosmosDb(this WebApplicationBuilder builder)
    {

        var services = builder.Services;
        var configuration = builder.Configuration;
        
        var ConnectionString = configuration["CosmosDB:ConnectionString"];
        var Database = configuration["CosmosDB:Database"];
        var Container = configuration["CosmosDB:Container"];
        var HostName = Environment.GetEnvironmentVariable("COSMOSDB_HOST") ?? "192.168.0.15";
        ConnectionString = ConnectionString.Replace("192.168.0.15", HostName);

        var httpClientHandler = new HttpClientHandler();
        httpClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
        {
            // Perform custom validation logic here, e.g. check the certificate thumbprint
            // against a known value or check that the certificate is issued by a trusted CA
            // and has not been revoked.

            // If the certificate is valid, return true, otherwise return false.
            return true;
        };

        var httpClient = new HttpClient(httpClientHandler);
        var cosmosClientOptions = new CosmosClientOptions
        {
            ConnectionMode = ConnectionMode.Gateway,
            HttpClientFactory = () => httpClient
        };

        var cosmosClient = new CosmosClient(ConnectionString, cosmosClientOptions);
        services.AddSingleton<CosmosClient>(cosmosClient);
        Database database = cosmosClient.CreateDatabaseIfNotExistsAsync(Database).Result;
        database.CreateContainerIfNotExistsAsync(Container, "/associateId");
        services.AddDbContext<ProfileDbContext>(option => option.UseCosmos(ConnectionString, Database));
    }
}