using System;
using ProdApi.Models;

namespace ProdApi.Repository;

public interface ITaskItemRepository
{
    // Define method signatures for task item repository operations here
    Task<IReadOnlyList<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task AddAsync(TaskItem taskItem);
    Task UpdateAsync(TaskItem taskItem);
    Task DeleteAsync(TaskItem taskItem);
}
