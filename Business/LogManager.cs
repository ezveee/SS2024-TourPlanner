﻿using Business.Interfaces;
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

	public void Delete(int id)
	{
		_repository.Delete(id);
	}

	public void Update(TourLog entity)
	{
		_repository.Update(MapToDataAccess(entity));
	}

	public List<TourLog>? GetLogsByTourId(int id)
	{
		List<DataAccess.Models.TourLog> dataAccessLogs = [.. context.TourLogs.Where(log => log.TourId == id)]; // collection expression to simplify .ToList()
		List<TourLog> businessLogs = [];

		foreach (DataAccess.Models.TourLog dataAccessLog in dataAccessLogs)
		{
			TourLog businessLog = MapToBusiness(dataAccessLog);
			businessLogs.Add(businessLog);
		}

		return businessLogs;
	}

	// TODO: ask if automapper is allowed
	// if so -> should conf be in own mapping project?
	// dependencies?
	// TODO: if no automapper -> move to util class
	public static DataAccess.Models.TourLog MapToDataAccess(TourLog log)
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

	// TODO: if no automapper -> move to util class
	public static TourLog MapToBusiness(DataAccess.Models.TourLog log)
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
