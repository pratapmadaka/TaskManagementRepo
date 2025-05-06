

using TaskManager.Domain.Entities;
using Microsoft.Azure.Cosmos;
using TaskManager.Application.Common.Interfaces;

namespace TaskManager.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private Container _container;
    public UserRepository(ICosmosDbService cosmosDbService)
    {
        _container = cosmosDbService.GetContainer("Users");
    }
    public async Task<Domain.Entities.User> AddUserAsync(Domain.Entities.User user)
    {
        var response = await _container.CreateItemAsync(user, new PartitionKey(user.TenentID));
        return response.Resource;
    }

    public async Task<TaskManager.Domain.Entities.User?> GetByEmailAsync(string email)
    {
        var query = new QueryDefinition("SELECT * from c WHERE c.email = @email").WithParameter("@email", email);
        var iterator = _container.GetItemQueryIterator<TaskManager.Domain.Entities.User>(query);
        if (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            if (response.Count > 0) return response.FirstOrDefault();
        }

        return null;

    }
}