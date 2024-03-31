using DataAccess.Data;
using DataAccess.Exceptions;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;


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
		SaveImageToFilesystem(entity);

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

	private static void SaveImageToFilesystem(Tour tour)
	{
		string imagePath = tour.RouteInformation;
		if (!File.Exists(imagePath))
		{
			throw new ImageNotFoundException();
		}

		using Image image = Image.FromFile(imagePath); // TODO: find out how to resolve CA1416
		string imageFileName = Guid.NewGuid().ToString() + ".png"; // TODO: possibly change the + ".png"
		string directoryPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Images"); // TODO: it is so ugly, but i struggled so hard to get here so i guess its staying 
		string newImagePath = Path.Combine(directoryPath, imageFileName);

		_ = Directory.CreateDirectory(directoryPath);
		image.Save(newImagePath); // TODO: add check if image was saved successfully

		tour.RouteInformation = imageFileName;
	}
}