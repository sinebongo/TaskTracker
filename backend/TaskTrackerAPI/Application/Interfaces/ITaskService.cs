using TaskTrackerAPI.Domain.Entities;

namespace TaskTrackerAPI.Application.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetAllAsync(string? q, string? sort);
        Task<TaskItem?> GetByIdAsync(int id);
        Task<TaskItem> CreateAsync(TaskItem task);
        Task UpdateAsync(int id, TaskItem updated);
        Task DeleteAsync(int id);
    }
}
