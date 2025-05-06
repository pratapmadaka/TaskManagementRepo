using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using TaskManager.Application.Common.Settings;


namespace TaskManager.Information.Data;

public class CosmosDbService : ICosmosDbService
{
    private readonly CosmosClient _cosmosClient;

    private readonly CosmosDbSettings _cosmosDbSettings;

    private Database? database;

    public CosmosDbService(IOptions<CosmosDbSettings> settings)
    {
        _cosmosDbSettings = settings.Value;
        var clientOptions = new CosmosClientOptions
        {
            HttpClientFactory = () =>
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (req, cert, chain, errors) => true
                };
                return new HttpClient(handler);
            }
        };
        _cosmosClient = new CosmosClient(_cosmosDbSettings.Account, _cosmosDbSettings.Key, clientOptions);

    }



    public async Task InitializeAsync()
    {
        var _database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_cosmosDbSettings.DatabaseName);
        await _database.Database.CreateContainerIfNotExistsAsync("Tasks", "/tenantId");
        await _database.Database.CreateContainerIfNotExistsAsync("Users", "/tenantId");
    }

    public Container GetContainer(string containerName)
    {
        return _cosmosClient.GetContainer(_cosmosDbSettings.DatabaseName, containerName);
    }
}