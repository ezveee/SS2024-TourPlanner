using Business.Extensions;
using Business.Interfaces;
using Business.Models;
using DataAccess.Interfaces;
using System.Linq.Expressions;

namespace Business.Services;

// TODO: figure out how to implement GenericService class
public class TourLogService(IRepository<DataAccess.Models.TourLog> repository) : IService<TourLog>
{
	public TourLog Create(TourLog entity)
	{
		DataAccess.Models.TourLog DataAccessTourLog = repository.Add(entity.MapLogToDataAccess());
		repository.SaveChanges();
		return DataAccessTourLog.MapLogToBusiness();
	}

	public TourLog Update(TourLog entity)
	{
		DataAccess.Models.TourLog DataAccessTourLog = repository.Update(entity.MapLogToDataAccess());
		repository.SaveChanges();
		return DataAccessTourLog.MapLogToBusiness();
	}

	public void Delete(int tourId)
	{
		IEnumerable<TourLog> logs = GetLogsByTourId(tourId);
		foreach (TourLog log in logs)
		{
			repository.Delete(log.LogId);
		}

		repository.SaveChanges();
	}

	public TourLog? GetById(int id)
	{
		DataAccess.Models.TourLog? log = repository.GetById(id);
		return log?.MapLogToBusiness();
	}

	public IEnumerable<TourLog>? GetAll()
	{
		IEnumerable<DataAccess.Models.TourLog>? dataAccessTourLogs = repository.GetAll();
		return dataAccessTourLogs is null ? (IEnumerable<TourLog>?)null : dataAccessTourLogs.Select(dataAccessTourLog => dataAccessTourLog.MapLogToBusiness());
	}

	public IEnumerable<TourLog>? Find(Expression<Func<TourLog, bool>> predicate)
	{
		IEnumerable<TourLog>? businessTourLogs = GetAll();
		return businessTourLogs is null ? (IEnumerable<TourLog>?)null : businessTourLogs.Where(predicate.Compile());
	}

	public IEnumerable<TourLog>? GetLogsByTourId(int id)
	{
		IEnumerable<DataAccess.Models.TourLog>? dataAccessTourLogs = repository.Find(log => log.TourId == id);
		return dataAccessTourLogs is null ? (IEnumerable<TourLog>?)null : dataAccessTourLogs.Select(dataAccessLog => dataAccessLog.MapLogToBusiness());
	}
}
