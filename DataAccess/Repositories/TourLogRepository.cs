using DataAccess.Data;
using DataAccess.Exceptions;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class TourLogRepository(TourPlannerContext context) : IRepository<TourLog>
{
	public TourLog? GetById(int id)
	{
		return context.TourLogs.Find(id);
	}

	public IEnumerable<TourLog> GetAll()
	{
		return context.TourLogs.OrderBy(log => log.LogId);
	}

	public void Add(TourLog entity)
	{
		_ = context.TourLogs.Add(entity);
		try
		{
			_ = context.SaveChanges();
		}
		catch (DbUpdateException)
		{
			Console.WriteLine("Error trying to add new entry.");
		}
	}

	public void Update(TourLog entity)
	{
		TourLog? existingEntity = context.TourLogs.Find(entity.LogId);
		if (existingEntity is not null)
		{
			context.Entry(existingEntity).CurrentValues.SetValues(entity);
			_ = context.SaveChanges();
		}
	}

	public void Delete(int id)
	{
		// TODO: try catch
		TourLog? existingEntity = context.TourLogs.Find(id);
		if (existingEntity is null)
		{
			throw new DeleteNonExistingEntityException();
		}

		_ = context.TourLogs.Remove(existingEntity);
		_ = context.SaveChanges();
	}
}