namespace TechOnIt.Application.Common.Interfaces.Repositories;

public interface IReportRepository
{
    Task<T> GetByIdAsync<T>(Guid id) where T : class;
    Task<T> GetByIdAsync<T>(Guid id, string tableName) where T : class;
    Task<T> GetByIdAsync<T>(Guid id, string tableName, string[] columnNames) where T : class;
    Task<List<T>> GetAllAsync<T>() where T : class;
    Task<List<T>> GetAllAsync<T>(string tableName) where T : class;
    Task<List<T>> GetAllAsync<T>(int skip, int take) where T : class;
    Task<List<T>> GetAllAsync<T>(string tableName, int skip, int take) where T : class;
    Task<int> GetCountAsync<T>() where T : class;
    Task<int> GetCountAsync<T>(string tableName) where T : class;
    Task<bool> ExistsAsync<T>(Guid id) where T : class;
    Task<bool> ExistsAsync<T>(Guid id, string tableName) where T : class;
    Task<List<T>> GetByConditionAsync<T>(string condition) where T : class;
    Task<List<T>> GetByConditionAsync<T>(string tableName, string condition) where T : class;
    Task<List<T>> GetByConditionAsync<T>(string tableName, string condition, string[] columnNames) where T : class;
    Task<List<T>> SelectSpecificColumnsAsync<T>(string[] columnNames) where T : class;
    Task<List<T>> SelectSpecificColumnsAsync<T>(string tableName, string[] columnNames) where T : class;
}