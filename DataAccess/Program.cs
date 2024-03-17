using DataAccess.Data;
using DataAccess.Models;

Console.WriteLine("Hello, World!");

using TourPlannerContext context = new();

Tour tour = new()
{
	Name = "Test",
	Description = "Test Description",
	From = "A",
	To = "B",
	TransportType = "Bike",
	Distance = 10,
	EstimatedTime = 2,
	RouteInformation = "img.png"
};

context.Tours.Add(tour);
context.SaveChanges();