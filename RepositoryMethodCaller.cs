using System.Data;
using TaskManager.Entities;
using TaskManager.Repository;

namespace TaskManager;

public class RepositoryMethodCaller
{
    private readonly ITaskRepository _taskRepository;

    public RepositoryMethodCaller(TaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task ShowAllTasksAsync()
    {
        var list = await _taskRepository.ShowAllTasksAsync();
        if (list.Count == 0)
        {
            Console.WriteLine("Empty");
            return;
        }
        
        foreach (var elem in list)
        {
            Console.WriteLine($"{elem.Id} - {elem.Title} - {elem.Description} - {elem.IsCompleted} - {elem.CreatedAt}");
        }
    }

    public async Task ShowUncompletedTasksAsync()
    {
        var list = await _taskRepository.ShowUncompletedTasksAsync();
        if (list.Count == 0)
        {
            Console.WriteLine("Empty");
            return;
        }
        
        foreach (var elem in list)
        {
            Console.WriteLine($"{elem.Id} - {elem.Title} - {elem.Description} - {elem.IsCompleted} - {elem.CreatedAt}");
        }
    }

    public async Task DeleteTaskAsync()
    {
        int id = InputNumber();
        await _taskRepository.DeleteTaskAsync(id);
    }

    public async Task CompleteTaskAsync()
    {
        int id = InputNumber();
        await _taskRepository.CompleteTaskAsync(id);
    }

    public async Task AddTaskAsync()
    {
        TaskEntity newTask = new TaskEntity();

        Console.WriteLine("Enter Task Name:");
        newTask.Title = Console.ReadLine();
        
        Console.WriteLine("Enter Task Description:");
        newTask.Description = Console.ReadLine();
        
        newTask.IsCompleted = false;
        newTask.CreatedAt = DateTime.Now;
        
        Console.WriteLine("Input \"y\" if you sure");
        if (Console.ReadLine().Equals("y"))
        {
            _taskRepository.AddNewTaskAsync(newTask);
        }
    }

    public int InputNumber()
    {
        int res;
        while (!int.TryParse(Console.ReadLine(), out res)) ;
        return res;
    }
}