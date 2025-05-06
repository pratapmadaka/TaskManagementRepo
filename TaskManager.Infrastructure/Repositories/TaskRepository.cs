

using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Repositories;
public class TaskRepository : ITaskRepository
{
    private readonly Container _container;
    private readonly ILogger<TaskRepository> _logger;
    public TaskRepository(ICosmosDbService cosmosDbService, ILogger<TaskRepository> logger)
    {
        _container = cosmosDbService.GetContainer("Tasks");
        _logger = logger;
    }

    public async Task<TaskItem> AddAsync(TaskItem task)
    {
        var response = await _container.CreateItemAsync(task, new PartitionKey(task.TenantId));
        return response.Resource;
    }
    public async Task DeleteAsync(string id, string tenantId)
    {
        await _container.DeleteItemAsync<TaskItem>(id, new PartitionKey(tenantId));
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync(string tenantId)
    {
        var query = new QueryDefinition("SELECT * from c where c.tenantId = @tenantId")
                        .WithParameter("@tenantId", tenantId);

        var iterator = _container.GetItemQueryIterator<TaskItem>(query);
        var results = new List<TaskItem>();

        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            results.AddRange(response.ToList());
        }

        return results;
    }
    public async Task<TaskItem?> GetByIdAsync(string id, string tenantId)
    {
        try
        {
            var response = await _container.ReadItemAsync<TaskItem>(id, new PartitionKey(tenantId));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            _logger.LogInformation($"Task with ID {id} not found for tenant {tenantId}.", ex, ex.Message);
            return null;
        }
    }
}