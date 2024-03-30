namespace Business.Interfaces;
public interface IManager<TEntity> where TEntity : class
{
	void Create(TEntity entity);
	void Update(TEntity entity);
	List<TEntity> GetAll();
	void Delete(TEntity entity);
}
