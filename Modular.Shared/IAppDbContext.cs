namespace Modular.Shared;

public interface IAppDbContext
{
    Task<IEnumerable<T>> GetAllDataAsync<T>() where T : class;
    Task<T> GetByIdasync<T>(int id) where T : class;
    Task<int> InsertAsync<T>(List<T> data) where T : class;
    Task<bool> UpdateAsync<T>(List<T> data) where T : class;
}
