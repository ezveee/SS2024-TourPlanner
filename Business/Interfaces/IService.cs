using Business.Models;
using System.Linq.Expressions;

namespace Business.Interfaces;
public interface IService<T> where T : class
{
	T Create(T entity);
	T Update(T entity);
	void Delete(int id);
	T? GetById(int id);
	IEnumerable<T>? GetAll();
	IEnumerable<T>? Find(Expression<Func<T, bool>> predicate);
	IEnumerable<TourLog>? GetLogsByTourId(int id); // TODO: change. i dont like this. ew. ewewewewew.
}
