using TaskManager.Entities;

namespace TaskManager.Repository;

public interface ITaskRepository
{
    public Task AddNewTaskAsync(TaskEntity task);
    public Task<List<TaskEntity>> ShowAllTasksAsync();
    public Task<List<TaskEntity>> ShowUncompletedTasksAsync();
    public Task CompleteTaskAsync(int id);
    public Task DeleteTaskAsync(int id);

}