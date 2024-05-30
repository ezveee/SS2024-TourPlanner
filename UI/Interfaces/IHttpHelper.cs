namespace UI.Interfaces;
public interface IHttpHelper<T> where T : class
{
	Task<T?> CreateDataAsync(T entity);
	Task<List<T>?> ReadDataAsync();
	Task<T?> ReadDataAsync(int id);
	Task<T?> UpdateDataAsync(T entity);
	Task DeleteDataAsync(int id);
}
