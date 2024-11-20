namespace TechOnIt.Application.Common.Interfaces.Repositories;

public interface IAdoRepository
{
    Task<int> ExecuteNonQueryAsync(string sql);
}