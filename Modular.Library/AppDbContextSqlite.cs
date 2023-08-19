using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using Modular.Shared;

namespace Modular.Library;

public sealed class AppDbContextSqlite : IAppDbContext
{
    private readonly string connectionString;

    public AppDbContextSqlite(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public Task<IEnumerable<T>> GetAllDataAsync<T>() where T : class
    {
        using (SqliteConnection conn = new SqliteConnection(connectionString))
        {
            return conn.GetAllAsync<T>();
        }
    }

    public Task<T> GetByIdasync<T>(int id) where T : class
    {
        using (SqliteConnection conn = new SqliteConnection(connectionString))
        {
            return conn.GetAsync<T>(id);
        }
    }

    public Task<int> InsertAsync<T>(List<T> data) where T : class
    {
        using (SqliteConnection conn = new SqliteConnection(connectionString))
        {
            return conn.InsertAsync(data);
        }
    }
    public Task<bool> UpdateAsync<T>(List<T> data) where T : class
    {
        using (SqliteConnection conn = new SqliteConnection(connectionString))
        {
            return conn.UpdateAsync(data);
        }
    }
}