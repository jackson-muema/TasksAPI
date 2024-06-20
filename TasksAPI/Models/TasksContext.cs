using Microsoft.EntityFrameworkCore;
namespace TasksAPI.Models
{
    public class TasksContext : DbContext
    {
        public TasksContext(DbContextOptions<TasksContext> options) : base(options)
        { 
        
        }

        public DbSet<TasksModel> TaskItems { get; set; } = null!;
    }
}
