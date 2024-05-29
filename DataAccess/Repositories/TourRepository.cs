using DataAccess.Data;
using DataAccess.Exceptions;
using DataAccess.Models;
using System.Drawing;


namespace DataAccess.Repositories;
public class TourRepository(TourPlannerContext context) : GenericRepository<Tour>(context)
{
	public override Tour Add(Tour entity)
	{
		SaveImageToFilesystem(entity);
		return base.Add(entity);
	}

	public override IEnumerable<Tour> GetAll()
	{
		return context.Tours.OrderBy(tour => tour.TourId);
	}

	//public void Update(Tour entity)
	//{
	//	Tour? existingEntity = context.Tours.Find(entity.TourId);
	//	if (existingEntity is not null)
	//	{
	//		context.Entry(existingEntity).CurrentValues.SetValues(entity);
	//		_ = context.SaveChanges();
	//	}
	//}

	// TODO: change method location
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