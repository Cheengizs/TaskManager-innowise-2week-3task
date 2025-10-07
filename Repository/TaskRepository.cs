using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using TaskManager.Entities;

namespace TaskManager.Repository;

public class TaskRepository : ITaskRepository
{
    private readonly ConnectionFactory _factory;

    public TaskRepository(ConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task AddNewTaskAsync(TaskEntity task)
    {
        using var dbConnection = _factory.GetConnection();
        string sqlCommandStr =
            "INSERT INTO Tasks (Title, Description, IsCompleted, CreatedAt) " +
            "VALUES (@Title, @Description, @IsCompleted, @CreatedAt)";
        
        await dbConnection.ExecuteAsync(sqlCommandStr, task);
    }

    public async Task<List<TaskEntity>> ShowAllTasksAsync()
    {
        using var dbConnection = _factory.GetConnection();
        string sqlCommandStr = "SELECT * FROM Tasks";

        return (await dbConnection.QueryAsync<TaskEntity>(sqlCommandStr)).ToList();
    }


    public async Task CompleteTaskAsync(int id)
    {
        using var dbConnection = _factory.GetConnection();
        string sqlCommandStr = "UPDATE Tasks SET IsCompleted = 1 WHERE Id = @Id";
        
        await dbConnection.ExecuteAsync(sqlCommandStr, new { Id = id });
    }

    public async Task DeleteTaskAsync(int id)
    {
        using var dbConnection = _factory.GetConnection();
        string sqlCommandStr = "DELETE FROM Tasks WHERE Id = @Id";

        await dbConnection.ExecuteAsync(sqlCommandStr, new { Id = id });
    }
}