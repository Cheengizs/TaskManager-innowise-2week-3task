using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using TaskManager.Entities;

namespace TaskManager.Repository;

public class TaskRepository : ITaskRepository
{
    private string _connectionString;

    public TaskRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task AddNewTaskAsync(TaskEntity task)
    {
        using IDbConnection dbConnection = new SqlConnection(_connectionString);
        dbConnection.Open();

        string sqlCommandStr =
            "INSERT INTO Tasks (Title, Description, IsCompleted, CreatedAt) " +
            "VALUES (@Title, @Description, @IsCompleted, @CreatedAt)";
        
        await dbConnection.ExecuteAsync(sqlCommandStr, task);
    }

    public async Task<List<TaskEntity>> ShowAllTasksAsync()
    {
        using IDbConnection dbConnection = new SqlConnection(_connectionString);
        dbConnection.Open();

        string sqlCommandStr = "SELECT * FROM Tasks";
        
        return (await dbConnection.QueryAsync<TaskEntity>(sqlCommandStr)).ToList();
    }


    public Task CompleteTaskAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTaskAsync(int id)
    {
        throw new NotImplementedException();
    }
}