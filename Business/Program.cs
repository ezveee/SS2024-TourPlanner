using Business.Interfaces;
using Business.Models;

namespace Business;
public static class Program
{
	private static void Main(string[] args)
	{
		IManager<Tour> manager = new TourManager();
		Tour tour = new()
		{
			Name = "image test",
			RouteInformation = @"C:\Users\Sara\Pictures\hallownest_map_hollow_knight_wiki.png"
		};

		manager.Create(tour);
	}
}
