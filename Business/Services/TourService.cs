using Business.Api;
using Business.Extensions;
using Business.Interfaces;
using Business.Models;
using DataAccess.Data;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Business.Services;
public class TourService : IService<Tour>
{
	// TODO: i dont think thats supposed to be here either :|
	private static readonly TourPlannerContext _context = new();
	// TODO: bruh thats not testable, change that shit
	private readonly IRepository<DataAccess.Models.Tour> _repository = new TourRepository(_context);

	private OrsDirectionsApi _ors = new OrsDirectionsApi();

	public void Create(Tour entity)
	{
		(entity.Distance, entity.EstimatedTime) = _ors.DirectionsGetRouteAsync(entity.From, entity.To, entity.TransportType).Result;

		_repository.Add(entity.MapTourToDataAccess());
	}

	//public List<Tour>? GetAll()
	//{
	//	List<DataAccess.Models.Tour> dataAccessTours = _repository.GetAll().ToList();
	//	List<Tour> businessTours = [];

	//	foreach (DataAccess.Models.Tour dataAccessTour in dataAccessTours)
	//	{
	//		Tour businessTour = dataAccessTour.MapTourToBusiness();
	//		businessTours.Add(businessTour);
	//	}

	//	return businessTours;
	//}

	public IEnumerable<Tour>? GetAll()
	{
		IEnumerable<DataAccess.Models.Tour> dataAccessTours = _repository.GetAll();
		return dataAccessTours.Select(dataAccessTour => dataAccessTour.MapTourToBusiness());
	}


	public Tour? GetById(int id)
	{
		DataAccess.Models.Tour? tour = _repository.GetById(id);

		return tour?.MapTourToBusiness();
	}

	public void Delete(int id)
	{
		_repository.Delete(id);
	}

	public void Update(Tour entity)
	{
		_repository.Update(entity.MapTourToDataAccess());
	}

	public List<TourLog>? GetLogsByTourId(int id)
	{
		List<DataAccess.Models.TourLog> dataAccessLogs = [.. _context.TourLogs.Where(log => log.TourId == id)]; // collection expression to simplify .ToList()
		List<TourLog> businessLogs = [];

		foreach (DataAccess.Models.TourLog dataAccessLog in dataAccessLogs)
		{
			TourLog businessLog = LogExtension.MapLogToBusiness(dataAccessLog);
			businessLogs.Add(businessLog);
		}

		return businessLogs;
	}
}
