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

        ConnectionFactory connFactory =
            new(configuration["Database:ConnectionString"]!, configuration["Database:DbType"]!);
        TaskRepository _taskRepository = new TaskRepository(connFactory);

        RepositoryMethodCaller repoMethodsCaller = new(_taskRepository);

        while (true)
        {
            Console.WriteLine("Press \"q\" to quit");
            Console.WriteLine("Press \"l\" to show all tasks");
            Console.WriteLine("Press \"d\" to delete task");
            Console.WriteLine("Press \"a\" to add task");
            Console.WriteLine("Press \"u\" to set task completed");
            Console.WriteLine("Press \"c\" to show all uncompleted tasks");

            string input = Console.ReadLine();

            switch (input)
            {
                
                case "q":
                    break;
                case "l":
                    await repoMethodsCaller.ShowAllTasksAsync();
                    break;
                case "c":
                    await repoMethodsCaller.ShowUncompletedTasksAsync();
                    break;
                case "d":
                    await repoMethodsCaller.DeleteTaskAsync();
                    break;
                case "u":
                    await repoMethodsCaller.CompleteTaskAsync();
                    break;
                case "a":
                    await repoMethodsCaller.AddTaskAsync();
                    break;
                default:
                    Console.WriteLine("Unknown input, please try again");
                    break;
            }
        }
    }
}