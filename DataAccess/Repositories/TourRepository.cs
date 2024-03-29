using DataAccess.Data;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;
public class TourRepository(TourPlannerContext context) : IRepository<Tour>
{
	public Tour? GetById(int id)
	{
		return context.Tours.Find(id);
	}

	public IEnumerable<Tour> GetAll()
	{
		return [.. context.Tours]; // TODO: be able to explain this
	}

	public void Add(Tour entity)
	{
		_ = context.Tours.Add(entity);
		try
		{
			_ = context.SaveChanges();
		}
		catch (DbUpdateException)
		{
			Console.WriteLine("Error trying to add new entry.");
		}
	}

	public void Update(Tour entity)
	{
		context.Entry(entity).State = EntityState.Modified;
		_ = context.SaveChanges();
	}

	public void Delete(Tour entity)
	{
		_ = context.Tours.Remove(entity);
		_ = context.SaveChanges();
	}
}