using DataAccess.Data;
using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories;

TourPlannerContext context = new();
IRepository<Tour> tourRepository = new TourRepository(context);

Tour newTour = new()
{
	Name = "Test2",
	Description = "Desc2",
	From = "C",
	To = "D",
	TransportType = "Foot",
	Distance = 1,
	EstimatedTime = 1,
	RouteInformation = "placeholder.png"
};

tourRepository.Add(newTour);

Tour? tour = tourRepository.GetById(2);

if (tour is not null)
{
	Console.WriteLine("Name: " + tour.Name);
	Console.WriteLine("Description: " + tour.Description);
	Console.WriteLine("From: " + tour.From);
	Console.WriteLine("To: " + tour.To);
	Console.WriteLine("Transport type: " + tour.TransportType);
	Console.WriteLine("Distance: " + tour.Distance);
	Console.WriteLine("Estimated time: " + tour.EstimatedTime);
	Console.WriteLine("Route information: " + tour.RouteInformation);
}


//IEnumerable<Tour> tours = tourRepository.GetAll();
//ArgumentNullException.ThrowIfNull(tours);

//foreach (Tour tour in tours)
//{
//	Console.WriteLine("Name: " + tour.Name);
//	Console.WriteLine("Description: " + tour.Description);
//	Console.WriteLine("From: " + tour.From);
//	Console.WriteLine("To: " + tour.To);
//	Console.WriteLine("Distance: " + tour.Distance);
//	Console.WriteLine("Estimated Time: " + tour.EstimatedTime);
//}
