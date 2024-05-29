using Business.Interfaces;
using Business.Models;
using Business.Services;
using DataAccess.Interfaces;
using DataAccess.Repositories;

namespace UI.Models;

public class TourModel
{
	public int TourId { get; set; }
	public string Name { get; set; } = null!;
	public string Description { get; set; } = null!;
	public string From { get; set; } = null!;
	public string To { get; set; } = null!;
	public string TransportType { get; set; } = null!;
	public float Distance { get; set; }
	public double EstimatedTime { get; set; }
	public string RouteInformation { get; set; } = null!;

	public List<Tour> Tours { get; set; }

	// TODO: i actually hate myself. i dont know what to do about this
	private static DataAccess.Data.TourPlannerContext tourPlannerContext = new();
	private static IRepository<DataAccess.Models.Tour> tourRepository = new TourRepository(tourPlannerContext);
	private static IService<Tour> tourService = new TourService(tourRepository);

	public TourModel()
	{
		Tours = tourService.GetAll().ToList();
	}

	public static void CreateTour(string name, string desc, string from, string to, string transportType, float distance, double time, string info)
	{
		Tour tour = new()
		{
			Name = name,
			Description = desc,
			From = from,
			To = to,
			TransportType = transportType,
			Distance = distance,
			EstimatedTime = time,
			RouteInformation = info
		};

		_ = tourService.Create(tour);
	}

	public static void UpdateTour(TourModel model)
	{
		if (model == null)
		{
			return;
		}

		Tour temp = new()
		{
			TourId = model.TourId,
			Name = model.Name,
			Description = model.Description,
			From = model.From,
			To = model.To,
			TransportType = model.TransportType,
			Distance = model.Distance,
			EstimatedTime = model.EstimatedTime,
			RouteInformation = model.RouteInformation
		};

		_ = tourService.Update(temp);
	}

	public static TourModel? GetTour(int id)
	{
		Tour? temp = tourService.GetById(id);

		if (temp == null)
		{
			return null;
		}

		TourModel tourModel = new()
		{
			TourId = id,
			Name = temp.Name,
			Description = temp.Description,
			From = temp.From,
			To = temp.To,
			TransportType = temp.TransportType,
			Distance = temp.Distance,
			EstimatedTime = temp.EstimatedTime,
			RouteInformation = temp.RouteInformation
		};

		return tourModel;
	}

	public static void DeleteTour(int id)
	{
		tourService.Delete(id);
	}
}
