using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace TaskManager;

public class ConnectionFactory
{
    private readonly string _connectionString;
    private readonly string _dbType;

    public ConnectionFactory(string connectionString, string dbType)
    {
        _connectionString = connectionString;
        _dbType = dbType;
    }
    
    public IDbConnection GetConnection()
    {
        return _dbType switch
        {
            "postgresql" => new NpgsqlConnection(_connectionString),
            "sqlserver" => new SqlConnection(_connectionString),
            _ => throw new ArgumentException($"Unknown connection type: {_dbType}"),
        };
    }
}