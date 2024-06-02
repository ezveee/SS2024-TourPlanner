using Business.Api;
using Business.Extensions;
using Business.Interfaces;
using Business.Models;
using DataAccess.Interfaces;
using System.Linq.Expressions;

namespace Business.Services;

public class TourService(IRepository<DataAccess.Models.Tour> repository) : IService<Tour>
{
    private OrsDirectionsApi _ors = new OrsDirectionsApi();

	public Tour Create(Tour entity)
	{
        (entity.Distance, entity.EstimatedTime, entity.RouteInformation) = _ors.DirectionsGetRouteAsync(entity.From, entity.To, entity.TransportType).Result;
        
		DataAccess.Models.Tour DataAccessTour = repository.Add(entity.MapTourToDataAccess());
		repository.SaveChanges();
		return DataAccessTour.MapTourToBusiness();
	}

	public Tour Update(Tour entity)
	{
		DataAccess.Models.Tour DataAccessTour = repository.Update(entity.MapTourToDataAccess());
		repository.SaveChanges();
		return DataAccessTour.MapTourToBusiness();
	}

	public void Delete(int id)
	{
		repository.Delete(id);
		repository.SaveChanges();
	}

	public Tour? GetById(int id)
	{
		DataAccess.Models.Tour? tour = repository.GetById(id);
		return tour?.MapTourToBusiness();
	}

	public IEnumerable<Tour>? GetAll()
	{
		IEnumerable<DataAccess.Models.Tour>? dataAccessTours = repository.GetAll();
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
		throw new NotImplementedException();
	}
}
