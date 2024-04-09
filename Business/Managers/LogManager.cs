using Business.Extensions;
using Business.Interfaces;
using Business.Models;
using DataAccess.Data;
using DataAccess.Interfaces;
using DataAccess.Repositories;

namespace Business.Managers;
public class LogManager : IManager<TourLog>
{
	// TODO: i dont think thats supposed to be here either :|
	private static readonly TourPlannerContext _context = new();
	// TODO: bruh thats not testable, change that shit
	private readonly IRepository<DataAccess.Models.TourLog> _repository = new TourLogRepository(_context);

	public void Create(TourLog entity)
	{
		_repository.Add(entity.MapLogToDataAccess());
	}

	public List<TourLog>? GetAll()
	{
		List<DataAccess.Models.TourLog> dataAccessLogs = _repository.GetAll().ToList();
		List<TourLog> businessLogs = [];

		foreach (DataAccess.Models.TourLog dataAccessLog in dataAccessLogs)
		{
			TourLog businessLog = dataAccessLog.MapLogToBusiness();
			businessLogs.Add(businessLog);
		}

		return businessLogs;
	}

	public TourLog? GetById(int id)
	{
		DataAccess.Models.TourLog? log = _repository.GetById(id);

		return log?.MapLogToBusiness();
	}

	public void Delete(int id)
	{
		_repository.Delete(id);
	}

	public void Update(TourLog entity)
	{
		_repository.Update(entity.MapLogToDataAccess());
	}

	public List<TourLog>? GetLogsByTourId(int id)
	{
		List<DataAccess.Models.TourLog> dataAccessLogs = [.. _context.TourLogs.Where(log => log.TourId == id)]; // collection expression to simplify .ToList()
		List<TourLog> businessLogs = [];

		foreach (DataAccess.Models.TourLog dataAccessLog in dataAccessLogs)
		{
			TourLog businessLog = dataAccessLog.MapLogToBusiness();
			businessLogs.Add(businessLog);
		}

		return businessLogs;
	}
}
