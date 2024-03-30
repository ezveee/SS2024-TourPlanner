using DataAccess.Data;
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
		return [.. context.TourLogs];
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
		context.Entry(entity).State = EntityState.Modified;
		_ = context.SaveChanges();
	}

	public void Delete(TourLog entity)
	{
		_ = context.TourLogs.Remove(entity);
		_ = context.SaveChanges();
	}
}