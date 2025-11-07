using Microsoft.EntityFrameworkCore;

namespace TaskTrackerAPI.Infrastructure.Database
{
    public class ApplicationDBContext : DbContext
    {
        DbSet<Task> Tasks => Set<Task>();

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions) { 
            
        }
    }
}
