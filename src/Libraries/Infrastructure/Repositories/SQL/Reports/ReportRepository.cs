using TechOnIt.Infrastructure.Persistence.Context;

namespace TechOnIt.Infrastructure.Repositories.SQL.Reports;

public class ReportRepository: IReportRepository
{
    private readonly IdentityContext _context;

    public ReportRepository(IdentityContext context)
    {
        _context = context;
    }

    public async Task<T> GetByIdAsync<T>(Guid id) where T : class
    {
        var sql = $"SELECT * FROM {typeof(T).Name} WHERE Id = {id}";
        return await _context.Set<T>().FromSqlRaw(sql).FirstOrDefaultAsync();
    }

    public async Task<T> GetByIdAsync<T>(Guid id,string tableName) where T : class
    {
        var sql = $"SELECT * FROM {tableName} WHERE Id = {id}";
        return await _context.Set<T>().FromSqlRaw(sql).FirstOrDefaultAsync();
    }

    public async Task<T> GetByIdAsync<T>(Guid id,string tableName, string[] columnNames) where T : class
    {
        var columns = string.Join(", ", columnNames);
        var sql = $"SELECT {columns} FROM {tableName} WHERE Id = {id}";
        return await _context.Set<T>().FromSqlRaw(sql).FirstOrDefaultAsync();
    }

    public async Task<List<T>> GetAllAsync<T>() where T : class
    {
        var sql = $"SELECT * FROM {typeof(T).Name}";
        return await _context.Set<T>().FromSqlRaw(sql).ToListAsync();
    }

    public async Task<List<T>> GetAllAsync<T>(string tableName) where T : class
    {
        var sql = $"SELECT * FROM {tableName}";
        return await _context.Set<T>().FromSqlRaw(sql).ToListAsync();
    }

    public async Task<List<T>> GetAllAsync<T>(int skip, int take) where T : class
    {
        var sql = $"SELECT * FROM {typeof(T).Name} ORDER BY Id OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY";
        return await _context.Set<T>().FromSqlRaw(sql).ToListAsync();
    }

    public async Task<List<T>> GetAllAsync<T>(string tableName, int skip, int take) where T : class
    {
        var sql = $"SELECT * FROM {tableName} ORDER BY Id OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY";
        return await _context.Set<T>().FromSqlRaw(sql).ToListAsync();
    }

    public async Task<int> GetCountAsync<T>() where T : class
    {
        var sql = $"SELECT COUNT(*) FROM {typeof(T).Name}";
        return await _context.Database.ExecuteSqlRawAsync(sql);
    }

    public async Task<int> GetCountAsync<T>(string tableName) where T : class
    {
        var sql = $"SELECT COUNT(*) FROM {tableName}";
        return await _context.Database.ExecuteSqlRawAsync(sql);
    }

    public async Task<bool> ExistsAsync<T>(Guid id) where T : class
    {
        var sql = $"SELECT CASE WHEN EXISTS (SELECT * FROM {typeof(T).Name} WHERE Id = {id}) THEN 1 ELSE 0 END";
        return (await _context.Database.ExecuteSqlRawAsync(sql)) == 1;
    }

    public async Task<bool> ExistsAsync<T>(Guid id,string tableName) where T : class
    {
        var sql = $"SELECT CASE WHEN EXISTS (SELECT * FROM {tableName} WHERE Id = {id}) THEN 1 ELSE 0 END";
        return (await _context.Database.ExecuteSqlRawAsync(sql)) == 1;
    }

    public async Task<List<T>> GetByConditionAsync<T>(string condition) where T : class
    {
        var sql = $"SELECT * FROM {typeof(T).Name} WHERE {condition}";
        return await _context.Set<T>().FromSqlRaw(sql).ToListAsync();
    }

    public async Task<List<T>> GetByConditionAsync<T>(string tableName, string condition) where T : class
    {
        var sql = $"SELECT * FROM {tableName} WHERE {condition}";
        return await _context.Set<T>().FromSqlRaw(sql).ToListAsync();
    }

    public async Task<List<T>> GetByConditionAsync<T>(string tableName, string condition, string[] columnNames) where T : class
    {
        var columns = string.Join(", ", columnNames);
        var sql = $"SELECT {columns} FROM {tableName} WHERE {condition}";
        return await _context.Set<T>().FromSqlRaw(sql).ToListAsync();
    }

    public async Task<List<T>> SelectSpecificColumnsAsync<T>(string[] columnNames) where T : class
    {
        var columns = string.Join(", ", columnNames);
        var sql = $"SELECT {columns} FROM {typeof(T).Name}";
        return await _context.Set<T>().FromSqlRaw(sql).ToListAsync();
    }

    public async Task<List<T>> SelectSpecificColumnsAsync<T>(string tableName,string[] columnNames) where T : class
    {
        var columns = string.Join(", ", columnNames);
        var sql = $"SELECT {columns} FROM {tableName}";
        return await _context.Set<T>().FromSqlRaw(sql).ToListAsync();
    }
}