

using TaskManager.Domain.Entities;

namespace TaskManager.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User> AddUserAsync(User user);
}