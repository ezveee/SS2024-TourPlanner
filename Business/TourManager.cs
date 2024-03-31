using Business.Interfaces;
using Business.Models;
using DataAccess.Data;
using DataAccess.Interfaces;
using DataAccess.Repositories;

namespace Business;
public class TourManager : IManager<Tour>
{
	private static readonly TourPlannerContext context = new();
	private readonly IRepository<DataAccess.Models.Tour> _repository = new TourRepository(context);

	public void Create(Tour entity)
	{
		_repository.Add(MapToDataAccess(entity));
	}

	public List<Tour>? GetAll()
	{
		List<DataAccess.Models.Tour> dataAccessTours = _repository.GetAll().ToList();
		List<Tour> businessTours = [];

		foreach (DataAccess.Models.Tour dataAccessTour in dataAccessTours)
		{
			Tour businessTour = MapToBusiness(dataAccessTour);
			businessTours.Add(businessTour);
		}

		return businessTours;
	}

	public Tour? GetById(int id)
	{
		DataAccess.Models.Tour? tour = _repository.GetById(id);

		return tour is not null ? MapToBusiness(tour) : null;
	}

	public void Delete(int id)
	{
		_repository.Delete(id);
	}

	public void Update(Tour entity)
	{
		_repository.Update(MapToDataAccess(entity));
	}

	public List<TourLog>? GetLogsByTourId(int id)
	{
		List<DataAccess.Models.TourLog> dataAccessLogs = [.. context.TourLogs.Where(log => log.TourId == id)]; // collection expression to simplify .ToList()
		List<TourLog> businessLogs = [];

		foreach (DataAccess.Models.TourLog dataAccessLog in dataAccessLogs)
		{
			TourLog businessLog = LogManager.MapToBusiness(dataAccessLog);
			businessLogs.Add(businessLog);
		}

		return businessLogs;
	}

	// TODO: ask if automapper is allowed
	// if so -> should conf be in own mapping project?
	// dependencies?
	// TODO: if no automapper -> move to util class
	public static DataAccess.Models.Tour MapToDataAccess(Tour tour)
	{
		return new()
		{
			TourId = tour.TourId,
			Name = tour.Name,
			Description = tour.Description,
			From = tour.From,
			To = tour.To,
			TransportType = tour.TransportType,
			Distance = tour.Distance,
			EstimatedTime = tour.EstimatedTime,
			RouteInformation = tour.RouteInformation
		};
	}

	// TODO: if no automapper -> move to util class
	public static Tour MapToBusiness(DataAccess.Models.Tour tour)
	{
		return new()
		{
			TourId = tour.TourId,
			Name = tour.Name,
			Description = tour.Description,
			From = tour.From,
			To = tour.To,
			TransportType = tour.TransportType,
			Distance = tour.Distance,
			EstimatedTime = tour.EstimatedTime,
			RouteInformation = tour.RouteInformation
		};
	}
}
