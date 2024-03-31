using Business.Interfaces;
using Business.Models;

namespace Business;

internal class Program
{
	private static void Main(string[] args)
	{
		Console.WriteLine("Test");

		IManager<Tour> manager = new TourManager();

		// CREATE TEST
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

		// READ TEST 1

		// READ TEST 2


		// UPDATE TEST
		Tour changedTour = manager.GetById(7);
		if (changedTour is null)
		{
			throw
		}
		changedTour.Description = "Not 'Desc2' anymore [hopefully :')]";
		changedTour.

		// DELETE TEST

	}
}
