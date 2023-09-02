using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskContext _context;

        public TaskController(TaskContext context)
        {
            _context = context;
        }

        // GET: api/Task
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTodoTasks()
        {
          if (_context.TodoTasks == null)
          {
              return NotFound();
          }
            return await _context.TodoTasks.FromSqlRaw("SELECT * FROM todo_tasks").ToListAsync();
        }

        // GET: api/Task/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTask(int id)
        {
            if (_context.TodoTasks == null)
            {
              return NotFound();
            }
            var task = await _context.TodoTasks.FromSqlRaw("SELECT * FROM todo_tasks WHERE id = {0}", id).FirstOrDefaultAsync();

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        // PUT: api/Task/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, Models.Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            var sql = "UPDATE todo_tasks SET naslov = {0}, opis = {1}, datum_ustvarjanja = {2}, opravljeno = {3} WHERE id = {4}";
            var parameters = new object[] { task.Naslov, task.Opis, task.DatumUstvarjanja, task.Opravljeno, id };

            await _context.Database.ExecuteSqlRawAsync(sql, parameters);


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Task
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Models.Task>> PostTask(Models.Task task)
        {
          if (_context.TodoTasks == null)
          {
              return Problem("Entity set 'TaskContext.TodoTasks'  is null.");
          }
            var sql = "INSERT INTO todo_tasks (naslov, opis, datum_ustvarjanja, opravljeno) VALUES ({0}, {1}, {2}, {3})";
            var parameters = new object[] { task.Naslov, task.Opis, task.DatumUstvarjanja, task.Opravljeno };
          
            await _context.Database.ExecuteSqlRawAsync(sql, parameters);

            return CreatedAtAction("GetTask", new { id = task.Id }, task);
        }

        // DELETE: api/Task/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            if (_context.TodoTasks == null)
            {
                return NotFound();
            }
            var task = await _context.TodoTasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            var sql = "DELETE FROM todo_tasks WHERE id = {0}";
            var parameters = new object[] { id };

            await _context.Database.ExecuteSqlRawAsync(sql, parameters);

            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return (_context.TodoTasks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
