using Business.Api;
using Business.Extensions;
using Business.Interfaces;
using Business.Models;
using DataAccess.Interfaces;
using System.Linq.Expressions;

namespace Business.Services;

public class TourService(IRepository<DataAccess.Models.Tour> tourRepository, IRepository<DataAccess.Models.TourLog> tourLogRepository) : IService<Tour>
{
	private readonly OrsDirectionsApi _ors = new();

	public Tour Create(Tour entity)
	{
		(entity.Distance, entity.EstimatedTime, entity.RouteInformation) = _ors.DirectionsGetRouteAsync(entity.From, entity.To, entity.TransportType).Result;

		DataAccess.Models.Tour DataAccessTour = tourRepository.Add(entity.MapTourToDataAccess());
		tourRepository.SaveChanges();
		return DataAccessTour.MapTourToBusiness();
	}

	public Tour Update(Tour entity)
	{
		(entity.Distance, entity.EstimatedTime, entity.RouteInformation) = _ors.DirectionsGetRouteAsync(entity.From, entity.To, entity.TransportType).Result;

		DataAccess.Models.Tour DataAccessTour = tourRepository.Update(entity.MapTourToDataAccess());
		tourRepository.SaveChanges();
		return DataAccessTour.MapTourToBusiness();
	}

	public void Delete(int id)
	{
		IEnumerable<TourLog>? logs = GetLogsByTourId(id);
		if (logs is not null)
		{
			foreach (TourLog log in logs)
			{
				tourLogRepository.Delete(log.LogId);
			}
		}

		tourRepository.Delete(id);
		tourRepository.SaveChanges();
	}

	public Tour? GetById(int id)
	{
		DataAccess.Models.Tour? tour = tourRepository.GetById(id);
		return tour?.MapTourToBusiness();
	}

	public IEnumerable<Tour>? GetAll()
	{
		IEnumerable<DataAccess.Models.Tour>? dataAccessTours = tourRepository.GetAll();
		return dataAccessTours is null ? (IEnumerable<Tour>?)null : dataAccessTours.Select(dataAccessTour => dataAccessTour.MapTourToBusiness());
	}

	public IEnumerable<Tour>? Find(Expression<Func<Tour, bool>> predicate)
	{
		IEnumerable<Tour>? businessTours = GetAll();
		return businessTours is null ? (IEnumerable<Tour>?)null : businessTours.Where(predicate.Compile());
	}

	// TODO: i know this is ugly. im really sorry but i dont know how else to do this atm
	// please let me formally apologise for my atrocities
	public IEnumerable<TourLog>? GetLogsByTourId(int id)
	{
		IEnumerable<DataAccess.Models.TourLog>? dataAccessTourLogs = tourLogRepository.Find(log => log.TourId == id);
		return dataAccessTourLogs is null ? (IEnumerable<TourLog>?)null : dataAccessTourLogs.Select(dataAccessLog => dataAccessLog.MapLogToBusiness());
	}
}
