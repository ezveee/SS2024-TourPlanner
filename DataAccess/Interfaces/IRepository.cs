using System.Linq.Expressions;

namespace DataAccess.Interfaces;
public interface IRepository<T> where T : class
{
	T Add(T entity);
	T Update(T entity);
	void Delete(int id);
	T? GetById(int id);
	IEnumerable<T>? GetAll();
	IEnumerable<T>? Find(Expression<Func<T, bool>> predicate);
	void SaveChanges();
}
