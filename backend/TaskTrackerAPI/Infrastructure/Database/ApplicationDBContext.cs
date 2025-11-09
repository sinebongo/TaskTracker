using Microsoft.EntityFrameworkCore;
using TaskTrackerAPI.Domain.Entities;

namespace TaskTrackerAPI.Infrastructure.Database
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<TaskItem> Tasks => Set<TaskItem>();

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions) { 
            
        }
    }
}
