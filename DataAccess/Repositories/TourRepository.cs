using DataAccess.Data;
using DataAccess.Exceptions;
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
		return context.Tours.OrderBy(tour => tour.TourId);
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
		Tour? existingEntity = context.Tours.Find(entity.TourId);
		if (existingEntity is not null)
		{
			context.Entry(existingEntity).CurrentValues.SetValues(entity);
			_ = context.SaveChanges();
		}
	}

	public void Delete(int id)
	{
		DataAccess.Models.Tour? existingEntity = context.Tours.Find(id);
		if (existingEntity is null)
		{
			throw new DeleteNonExistingEntityException();
		}

		_ = context.Tours.Remove(existingEntity);
		_ = context.SaveChanges();
	}
}