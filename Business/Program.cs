using Business.Exceptions;
using Business.Interfaces;
using Business.Models;

namespace Business;

public static class Program
{
	private static void Main(string[] args)
	{
		Console.WriteLine("Test");
		IManager<Tour> manager = new TourManager();

		// CREATE TEST - SUCCEEDED
		//Tour tour = new()
		//{
		//	Name = "CreateTest1",
		//	Description = "Test",
		//	From = "Test",
		//	To = "Test",
		//	TransportType = "Test",
		//	Distance = 69.420f,
		//	EstimatedTime = 6.9,
		//	RouteInformation = "uwu"
		//};
		//manager.Create(tour);

		// CREATE TEST (should fail) - FAILED SUCCESSFULLY
		//Tour tour = new()
		//{
		//	To = "Failing Test",
		//	TransportType = "Failing Test",
		//	Distance = 69.420f,
		//	EstimatedTime = 6.9,
		//	RouteInformation = "uwu pls fail"
		//};
		//manager.Create(tour);

		// READ TEST 1 - SUCCEEDED
		//List<Tour>? tours = manager.GetAll();
		//if (tours is null)
		//{
		//	throw new DatabaseNullRetrievalException("No tours to be retrieved.");
		//}

		//foreach (Tour tour in tours)
		//{
		//	Console.WriteLine(new string('-', 20));
		//	Console.WriteLine("TourId: " + tour.TourId);
		//	Console.WriteLine("Name: " + tour.Name);
		//	Console.WriteLine("Description: " + tour.Description);
		//	Console.WriteLine("From: " + tour.From);
		//	Console.WriteLine("To: " + tour.To);
		//	Console.WriteLine("TransportType: " + tour.TransportType);
		//	Console.WriteLine("Distance: " + tour.Distance);
		//	Console.WriteLine("EstimatedTime: " + tour.EstimatedTime);
		//	Console.WriteLine("RouteInformation: " + tour.RouteInformation);
		//}

		// READ TEST 2 - SUCCEEDED
		//Tour? tour = manager.GetById(7);
		//if (tour is null)
		//{
		//	throw new DatabaseNullRetrievalException("Tour with queried ID does not exist.");
		//}

		//Console.WriteLine(new string('-', 20));
		//Console.WriteLine("TourId: " + tour.TourId);
		//Console.WriteLine("Name: " + tour.Name);
		//Console.WriteLine("Description: " + tour.Description);
		//Console.WriteLine("From: " + tour.From);
		//Console.WriteLine("To: " + tour.To);
		//Console.WriteLine("TransportType: " + tour.TransportType);
		//Console.WriteLine("Distance: " + tour.Distance);
		//Console.WriteLine("EstimatedTime: " + tour.EstimatedTime);
		//Console.WriteLine("RouteInformation: " + tour.RouteInformation);


		// UPDATE TEST - SUCCEEDED
		//Tour? changedTour = manager.GetById(7);
		//if (changedTour is null)
		//{
		//	throw new DatabaseNullRetrievalException("Tour with queried ID could does not exist.");
		//}
		//changedTour.Description = "Not 'Desc2' anymore [hopefully :')]";
		//changedTour.From = "P";
		//manager.Update(changedTour);

		// DELETE TEST
		Tour? tour = manager.GetById(12);
		if (tour is null)
		{
			throw new DatabaseNullRetrievalException("Tour with queried ID does not exist.");
		}

		Console.WriteLine(new string('-', 20));
		Console.WriteLine("TourId: " + tour.TourId);
		Console.WriteLine("Name: " + tour.Name);
		Console.WriteLine("Description: " + tour.Description);
		Console.WriteLine("From: " + tour.From);
		Console.WriteLine("To: " + tour.To);
		Console.WriteLine("TransportType: " + tour.TransportType);
		Console.WriteLine("Distance: " + tour.Distance);
		Console.WriteLine("EstimatedTime: " + tour.EstimatedTime);
		Console.WriteLine("RouteInformation: " + tour.RouteInformation);

		manager.Delete(12);

		Tour? deleteTest = manager.GetById(10);
		if (deleteTest is null)
		{
			Console.WriteLine("Deletion succeeded.");
		}
	}
}
