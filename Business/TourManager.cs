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
		List<Tour> businessLogs = [];

		foreach (DataAccess.Models.Tour dataAccessTour in dataAccessTours)
		{
			Tour businessLog = MapToBusiness(dataAccessTour);
			businessLogs.Add(businessLog);
		}

		return businessLogs;
	}

	public Tour? GetById(int id)
	{
		DataAccess.Models.Tour? tour = _repository.GetById(id);

		return tour is not null ? MapToBusiness(tour) : null;
	}

	public void Delete(Tour entity)
	{
		_repository.Delete(MapToDataAccess(entity));
	}

	public void Update(Tour entity)
	{
		_repository.Update(MapToDataAccess(entity));
	}

	// TODO: ask if automapper is allowed
	// if so -> should conf be in own mapping project?
	// dependencies?
	private static DataAccess.Models.Tour MapToDataAccess(Tour tour)
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

	private static Tour MapToBusiness(DataAccess.Models.Tour tour)
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
