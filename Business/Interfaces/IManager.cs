namespace Business.Interfaces;
public interface IManager<TEntity> where TEntity : class
{
	void Create(TEntity entity);
	List<TEntity>? GetAll();
	TEntity? GetById(int id);
	void Update(TEntity entity);
	void Delete(int id);
}
