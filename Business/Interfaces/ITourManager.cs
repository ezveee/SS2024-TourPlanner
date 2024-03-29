using DataAccess.Models;

namespace Business.Interfaces;
public interface ITourManager
{
	void CreateTour(string name, string description,
		string from, string to, string transportType,
		float distance, double estimatedTime,
		string routeInformation); // TODO: create strict for tour details
	void UpdateTour(Tour modifiedTour);
	List<Tour> GetTours();
	void DeleteTour(Tour tour);
}
