using Microsoft.EntityFrameworkCore;
namespace ToDoApp.Data;
public class TaskContext : DbContext
{
    public TaskContext(DbContextOptions<TaskContext> options)
        : base(options)
    {}

    public DbSet<Models.Task> TodoTasks { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the 'Task' entity as a keyless entity
            modelBuilder.Entity<Models.Task>().HasKey(t => new { t.Id});
        }

}