using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using TaskManager.Application.Common.Settings;
namespace TaskManager.Information.Data;

public class CosmosDbContext
{
    public readonly CosmosClient Client;
    public readonly Container UserContainer;

    public CosmosDbContext(IOptions<CosmosDbSettings> settings)
    {
        var config = settings.Value;
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
        Client = new CosmosClient(config.Account, config.Key, clientOptions);

        var database = Client.CreateDatabaseIfNotExistsAsync(config.DatabaseName).Result;

        UserContainer = database.Database.CreateContainerIfNotExistsAsync(
            id: "Users",
            partitionKeyPath: "/username"
        ).Result.Container;
    }

}