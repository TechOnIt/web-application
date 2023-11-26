namespace TechOnIt.Infrastructure.Repositories.SQL.HeavyTransaction;

public interface IAdoRepository
{
    Task<int> ExecuteNonQueryAsync(string sql);
}