using Business.Interfaces;
using Business.Models;
using DataAccess.Data;
using DataAccess.Interfaces;
using DataAccess.Repositories;

namespace Business;
public class LogManager : IManager<TourLog>
{
	private static readonly TourPlannerContext context = new();
	private readonly IRepository<DataAccess.Models.TourLog> _repository = new TourLogRepository(context);

	public void Create(TourLog entity)
	{
		_repository.Add(MapToDataAccess(entity));
	}

	public List<TourLog>? GetAll()
	{
		List<DataAccess.Models.TourLog> dataAccessLogs = _repository.GetAll().ToList();
		List<TourLog> businessLogs = [];

		foreach (DataAccess.Models.TourLog dataAccessLog in dataAccessLogs)
		{
			TourLog businessLog = MapToBusiness(dataAccessLog);
			businessLogs.Add(businessLog);
		}

		return businessLogs;
	}

	public TourLog? GetById(int id)
	{
		DataAccess.Models.TourLog? log = _repository.GetById(id);

		return log is not null ? MapToBusiness(log) : null;
	}

	public void Delete(TourLog entity)
	{
		_repository.Delete(MapToDataAccess(entity));
	}

	public void Update(TourLog entity)
	{
		_repository.Update(MapToDataAccess(entity));
	}

	// TODO: ask if automapper is allowed
	// if so -> should conf be in own mapping project?
	// dependencies?
	private static DataAccess.Models.TourLog MapToDataAccess(TourLog log)
	{
		return new()
		{
			LogId = log.LogId,
			Date = log.Date,
			Comment = log.Comment,
			Difficulty = log.Difficulty,
			Distance = log.Distance,
			Time = log.Time,
			Rating = log.Rating,
			TourId = log.TourId,

			// TODO: Tour = get tour by id
		};
	}

	private static TourLog MapToBusiness(DataAccess.Models.TourLog log)
	{
		return new()
		{
			LogId = log.LogId,
			Date = log.Date,
			Comment = log.Comment,
			Difficulty = log.Difficulty,
			Distance = log.Distance,
			Time = log.Time,
			Rating = log.Rating,
			TourId = log.TourId,
		};
	}
}
