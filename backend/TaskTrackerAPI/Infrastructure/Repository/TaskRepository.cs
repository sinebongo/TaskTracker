using Microsoft.EntityFrameworkCore;
using TaskTrackerAPI.Application.Interfaces;
using TaskTrackerAPI.Domain.Entities;
using TaskTrackerAPI.Infrastructure.Database;

namespace TaskTrackerAPI.Infrastructure.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private ApplicationDBContext _db;

        public TaskRepository(ApplicationDBContext applicationDBContext) 
        { 
            _db = applicationDBContext;
        }

        public async Task<TaskItem> AddTaskAsync(TaskItem task)
        {
            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();
            return task;
        }

        public async Task DeleteTaskAsync(TaskItem task)
        {
            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync();

        }

        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            return await _db.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        }
            
        public async Task<IList<TaskItem>> GetTasksAsync(string? search, string? sort)
        {

            var query = _db.Tasks.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                query = query.Where(t => t.Title.ToLower().Contains(search) ||
                                         (t.Description ?? "").ToLower().Contains(search));
            }

            query = sort == "dueDate:desc"
                ? query.OrderByDescending(t => t.DueDate)
                : query.OrderBy(t => t.DueDate);

            return await query.ToListAsync();
        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            _db.Tasks.Update(task);
            await _db.SaveChangesAsync();
        }
    }
}
