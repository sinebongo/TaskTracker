using TaskTrackerAPI.Application.Interfaces;
using TaskTrackerAPI.Domain.Entities;

namespace TaskTrackerAPI.Application.Services
{
    public class TaskService : ITaskService
    {
        public Task<TaskItem> CreateAsync(TaskItem task)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TaskItem>> GetAllAsync(string? q, string? sort)
        {
            throw new NotImplementedException();
        }

        public Task<TaskItem?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, TaskItem updated)
        {
            throw new NotImplementedException();
        }
    }
}
