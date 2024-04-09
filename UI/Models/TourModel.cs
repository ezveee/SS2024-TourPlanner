using Business.Interfaces;
using Business.Managers;
using Business.Models;

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

	public IManager<Tour> manager = new TourManager();

	public TourModel()
	{
		Tours = manager.GetAll();
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

		IManager<Tour> manager = new TourManager();
		manager.Create(tour);
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

		IManager<Tour> manager = new TourManager();
		manager.Update(temp);
	}

	public static TourModel? GetTour(int id)
	{
		IManager<Tour> manager = new TourManager();
		Tour? temp = manager.GetById(id);

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
		IManager<Tour> manager = new TourManager();
		manager.Delete(id);
	}
}
