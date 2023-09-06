using Microsoft.EntityFrameworkCore;
using ToDoApp.Interfaces;
using ToDoApp.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ToDoApp.Repository
{
        public class TaskRepository : ITaskRepository
    {
        private readonly TaskContext _context;
        public TaskRepository(TaskContext context){
            _context = context;
        }
        public async Task<int> DeleteTask(int id)
        {
            var sql = "DELETE FROM todo_tasks WHERE id = {0}";
            var parameters = new object[] { id };

            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
            return id;
        }
      /*  public async Task<bool> UpdateTask(int id, Models.Task task){
            var sql = "UPDATE todo_tasks SET naslov = {0}, opis = {1}, datum_ustvarjanja = {2}, opravljeno = {3} WHERE id = {4}";
            var parameters = new object[] { task.Naslov, task.Opis, task.DatumUstvarjanja, task.Opravljeno, id };
            await _context.Database.ExecuteSqlRawAsync(sql, parameters);
            return true;
        }*/

        public ICollection<Models.Task> GetAllTasks(){     
        try
            {
                var tasks = _context.TodoTasks.FromSqlRaw("SELECT * FROM todo_tasks").ToList();
                return tasks;
            }
        catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Models.Task>(); // Return an empty collection or handle the error gracefully
            }
       }
        public async Task<Models.Task> GetTaskById(int id)
        {
            var task = await _context.TodoTasks.FromSqlRaw("SELECT * FROM todo_tasks WHERE id = {0}", id).FirstOrDefaultAsync();
            if (task == null){
                return NotFound();
            }
            return task;

        }
        public async Task<Models.Task> CreateTask(Models.Task task)
        {
            var sql = "INSERT INTO todo_tasks (naslov, opis, datum_ustvarjanja, opravljeno) VALUES ({0}, {1}, {2}, {3})";
            var parameters = new object[] { task.Naslov, task.Opis, task.DatumUstvarjanja, task.Opravljeno };
            var generatedId = await _context.Database.ExecuteSqlRawAsync(sql, parameters);
            var newTask = await _context.TodoTasks
            .FromSqlRaw("SELECT * FROM todo_tasks WHERE naslov = {0} AND opis = {1} AND datum_ustvarjanja = {2}", task.Naslov, task.Opis, task.DatumUstvarjanja)
            .FirstOrDefaultAsync();
            task.Id = generatedId;
             if (newTask == null){
                return NotFound();
            }
            return newTask;
        }
        private Models.Task NotFound()
        {
            throw new NotImplementedException();
        }

        Task<bool> ITaskRepository.UpdateTask(Models.Task task)
        {
            throw new NotImplementedException();
        }
    }


    }