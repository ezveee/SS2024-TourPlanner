using DataAccess.Data;
using DataAccess.Exceptions;
using DataAccess.Interfaces;
using System.Linq.Expressions;

namespace DataAccess.Repositories;
public abstract class GenericRepository<T>(TourPlannerContext context) : IRepository<T> where T : class
{
	public virtual T Add(T entity)
	{
		return context.Add(entity).Entity;
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
		return [.. context.Set<T>()];
	}

	public virtual IEnumerable<T>? Find(Expression<Func<T, bool>> predicate)
	{
		return [.. context.Set<T>()
			.AsQueryable()
			.Where(predicate)];
	}

	public virtual T? GetById(int id)
	{
		return context.Find<T>(id);
	}

	public virtual void SaveChanges()
	{
		_ = context.SaveChanges();
	}

	public virtual T Update(T entity)
	{
		return context.Update(entity).Entity;
	}
}
