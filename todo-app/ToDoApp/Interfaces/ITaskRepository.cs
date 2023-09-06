using ToDoApp.Models;


namespace ToDoApp.Interfaces {
    public interface ITaskRepository
    {
        ICollection<Models.Task> GetAllTasks();
        Task<Models.Task> GetTaskById(int id);
        Task<Models.Task> CreateTask(Models.Task task);
        Task<bool> UpdateTask(Models.Task task);
        Task<int> DeleteTask(int id);
    }
}