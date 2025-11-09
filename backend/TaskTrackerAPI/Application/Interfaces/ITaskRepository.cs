using TaskTrackerAPI.Domain.Entities;

namespace TaskTrackerAPI.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task<IList<TaskItem>> GetTasksAsync(string? search, string? sort);
        Task<TaskItem?> GetTaskByIdAsync(int id);

        Task<TaskItem> AddTaskAsync(TaskItem task);
        
        Task UpdateTaskAsync(TaskItem task);

        Task DeleteTaskAsync(TaskItem task);
    }
}
