using System;
using Microsoft.EntityFrameworkCore;
using ProdApi.Context;
using ProdApi.Models;

namespace ProdApi.Repository;

public sealed class TaskItemRepository : ITaskItemRepository
{
    private readonly TaskDbContext _context;
    public TaskItemRepository(TaskDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<TaskItem>> GetAllAsync()
    {
        return await _context.Tasks.ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id)
    {
        return await _context.Tasks.FindAsync(id);
    }
    public async Task AddAsync(TaskItem taskItem)
    {
        _context.Tasks.Add(taskItem);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(TaskItem taskItem)
    {
        _context.Tasks.Update(taskItem);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(TaskItem taskItem)
    {

        _context.Tasks.Remove(taskItem);
        await _context.SaveChangesAsync();
    }
}
