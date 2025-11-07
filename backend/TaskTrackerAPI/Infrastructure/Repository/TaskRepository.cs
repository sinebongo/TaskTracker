using TaskTrackerAPI.Application.Interfaces;
using TaskTrackerAPI.Domain.Entities;

namespace TaskTrackerAPI.Infrastructure.Repository
{
    public class TaskRepository : ITaskRepository
    {
        public Task<TaskItem> AddTaskAsync(TaskItem task)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTaskAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<TaskItem>> GetTasksAsync(string? search, string? sort)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTaskAsync(TaskItem task)
        {
            throw new NotImplementedException();
        }
    }
}
