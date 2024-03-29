using Business.Models;

namespace Business.Interfaces;
public interface ITourManager
{
	void CreateTour(Tour tour);
	void UpdateTour(Tour modifiedTour);
	List<Tour> GetTours();
	void DeleteTour(Tour tour);
}
