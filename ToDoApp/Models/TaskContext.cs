using Microsoft.EntityFrameworkCore;
namespace ToDoApp.Models;

public class TaskContext : DbContext
{
    public TaskContext(DbContextOptions<TaskContext> options)
        : base(options)
    {}

    public DbSet<Task> TodoTasks { get; set; } = null!;
}