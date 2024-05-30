namespace UI.Interfaces;
public interface IManager<T> where T : class
{
	Task<T?> CreateAsync(T entity);
	Task<List<T>?> GetAllAsync();
	Task<T?> GetByIdAsync(int id);
	Task<T?> UpdateAsync(T entity);
	void DeleteAsync(T entity);
}
