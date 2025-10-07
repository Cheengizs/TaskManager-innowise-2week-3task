using TaskManager.Entities;
using TaskManager.Repository;
using Microsoft.Extensions.Configuration;

namespace TaskManager;

public class Program
{
    public static async Task Main(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        
        ConnectionFactory connFactory = new(configuration["Database:ConnectionString"]!, configuration["Database:DbType"]!);

        TaskRepository _taskRepository = new TaskRepository(connFactory);

        while (true)
        {
            string input = Console.ReadLine();

            if (input == "q") break;
            if (input == "l")
            {
                var list = await _taskRepository.ShowAllTasksAsync();
                foreach (var elem in list)
                {
                    Console.WriteLine(
                        $"{elem.Id} - {elem.Title} - {elem.Description} - {elem.IsCompleted} - {elem.CreatedAt}");
                }
            }
            else if (input == "i")
            {
                _taskRepository.AddNewTaskAsync(new TaskEntity()
                {
                    Title = Console.ReadLine(),
                    Description = Console.ReadLine(),
                    CreatedAt = DateTime.Now,
                    IsCompleted = false,
                });
            }
            else if (input == "d") _taskRepository.DeleteTaskAsync(int.Parse(Console.ReadLine()));
            else if (input == "c") _taskRepository.CompleteTaskAsync(int.Parse(Console.ReadLine()));
        }
    }
}