using Business.Models;

namespace Business.Interfaces;
public interface IService<TEntity> where TEntity : class
{
	void Create(TEntity entity);
	IEnumerable<TEntity>? GetAll();
	TEntity? GetById(int id);
	List<TourLog>? GetLogsByTourId(int id); // TODO: change. i dont like this. ew. ewewewewew.
	void Update(TEntity entity);
	void Delete(int id);
}
