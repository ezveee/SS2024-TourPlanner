using DataAccess.Data;
using DataAccess.Exceptions;
using DataAccess.Interfaces;
using System.Linq.Expressions;

namespace DataAccess.Repositories;
public abstract class GenericRepository<T>(TourPlannerContext context) : IRepository<T> where T : class
{
	protected TourPlannerContext _context = context;

	public virtual T Add(T entity)
	{
		return _context.Add(entity).Entity;
	}

	public virtual void Delete(int id)
	{
		T? existingEntity = context.Set<T>().Find(id);
		if (existingEntity is null)
		{
			throw new DeleteNonExistingEntityException();
		}

		_ = context.Set<T>().Remove(existingEntity);
	}

	public virtual IEnumerable<T>? GetAll()
	{
		return [.. _context.Set<T>()];
	}

	public virtual IEnumerable<T>? Find(Expression<Func<T, bool>> predicate)
	{
		return [.. _context.Set<T>()
			.AsQueryable()
			.Where(predicate)];
	}

	public virtual T? GetById(int id)
	{
		return _context.Find<T>(id);
	}

	public virtual void SaveChanges()
	{
		_ = _context.SaveChanges();
	}

	public virtual T Update(T entity)
	{
		return _context.Update(entity).Entity;
	}
}
