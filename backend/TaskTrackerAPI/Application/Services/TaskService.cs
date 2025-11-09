using TaskTrackerAPI.Application.Interfaces;
using TaskTrackerAPI.Domain.Entities;

namespace TaskTrackerAPI.Application.Services
{
    public class TaskService : ITaskService
    {
        private ITaskRepository _repo;
        private IConfiguration _config;

        public TaskService(ITaskRepository taskRepository, IConfiguration configuration) 
        {
            _repo = taskRepository;
            _config = configuration;
        }
        public Task<TaskItem> CreateAsync(TaskItem task)
        {
            task.CreatedAt = DateTime.UtcNow;
            return _repo.AddTaskAsync(task);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetTaskByIdAsync(id)
            ?? throw new KeyNotFoundException("Task not found.");

            await _repo.DeleteTaskAsync(entity);
        }

        public async Task<IList<TaskItem>> GetAllAsync(string? q, string? sort)
        {
            return await _repo.GetTasksAsync(q, sort);
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _repo.GetTaskByIdAsync(id);
        }

        public async Task UpdateAsync(int id, TaskItem updated)
        {
            var existing = await _repo.GetTaskByIdAsync(id)
           ?? throw new KeyNotFoundException("Task not found.");

            existing.Title = updated.Title;
            existing.Description = updated.Description;
            existing.Status = updated.Status;
            existing.Priority = updated.Priority;
            existing.DueDate = updated.DueDate;

            await _repo.UpdateTaskAsync(existing);
        }
    }
}
