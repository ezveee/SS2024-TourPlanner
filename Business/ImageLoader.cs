using Business.Exceptions;
using DataAccess.Data;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using System.Drawing;

namespace Business;
public class ImageLoader
{
	private static readonly TourPlannerContext _context = new();
	private readonly IRepository<DataAccess.Models.Tour> _repository = new TourRepository(_context);

	public Image LoadTourImage(int id)
	{
		DataAccess.Models.Tour? tour = _repository.GetById(id);
		if (tour is null)
		{
			throw new DatabaseNullRetrievalException("No tour with the queried id exists.");
		}
		string imagePath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "Images", tour.RouteInformation);

		return File.Exists(imagePath) ? Image.FromFile(imagePath) : throw new FileNotFoundException("Image file not found.", imagePath);
	}
}
