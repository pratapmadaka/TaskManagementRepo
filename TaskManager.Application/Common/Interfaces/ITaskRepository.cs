using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces;

public interface ITaskRepository
{
    Task<IEnumerable<TaskItem>> GetAllAsync(string tenantId);
    Task<TaskItem?> GetByIdAsync(string id, string tenantId);
    Task<TaskItem> AddAsync(TaskItem task);
    Task DeleteAsync(string id, string tenantId);
}