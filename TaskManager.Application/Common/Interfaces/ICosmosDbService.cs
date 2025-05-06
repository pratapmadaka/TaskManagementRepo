using Microsoft.Azure.Cosmos;
using TaskManager.Domain.Entities;

public interface ICosmosDbService
{
    Container GetContainer(string containerName);
    Task InitializeAsync();
}