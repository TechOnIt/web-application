using Microsoft.Data.SqlClient;
using TechOnIt.Application.Common.Interfaces.Repositories;
using TechOnIt.Infrastructure.Common.Helpers;

namespace TechOnIt.Infrastructure.Repositories.SQL.HeavyTransaction;

/// <summary>
/// not using this class and method for get operations - it is just for insert - update - delete operations
/// </summary>
public class AdoRepository : IAdoRepository
{
    private readonly string _connectionString;
    public AdoRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public AdoRepository()
    {
        _connectionString = ContextHelper.GetConnectionString();
    }

    public async Task<int> ExecuteNonQueryAsync(string sql)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                try
                {
                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected;
                }
                catch (SqlException exp)
                {
                    throw;
                }
            }
        }
    }
}