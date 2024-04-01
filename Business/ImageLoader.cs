using Business.Exceptions;
using DataAccess.Data;
using DataAccess.Interfaces;
using DataAccess.Repositories;

namespace Business;
public class ImageLoader
{
	private static readonly TourPlannerContext _context = new();
	private readonly IRepository<DataAccess.Models.Tour> _repository = new TourRepository(_context);

	public string GetImagePath(int id)
	{
		DataAccess.Models.Tour? tour = _repository.GetById(id);
		return tour is null
			? throw new DatabaseNullRetrievalException("No tour with the queried id exists.")
			: Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Images", tour.RouteInformation);
	}

	public void DeleteImage(int id)
	{
		string filePath = GetImagePath(id);
		if (!File.Exists(filePath))
		{
			throw new FileNotFoundException("The image file could not be located.");
		}

		File.Delete(filePath);
	}

	//public Image LoadTourImage(int id)
	//{
	//	DataAccess.Models.Tour? tour = _repository.GetById(id);
	//	if (tour is null)
	//	{
	//		throw new DatabaseNullRetrievalException("No tour with the queried id exists.");
	//	}
	//	string imagePath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Images", tour.RouteInformation);

	//	return File.Exists(imagePath) ? Image.FromFile(imagePath) : throw new FileNotFoundException("Image file not found.", imagePath);
	//}
}
