using Business.Interfaces;
using Business.Models;
using Business.Services;
using DataAccess.Interfaces;
using DataAccess.Repositories;

namespace UI.Models;

public class TourLogModel
{
	public int LogId { get; set; }
	public int TourId { get; set; }
	public DateTime Date { get; set; }
	public string Comment { get; set; } = null!;
	public int Difficulty { get; set; }
	public float Distance { get; set; }
	public double Time { get; set; }
	public int Rating { get; set; }

	public List<TourLog> TourLogs { get; set; }

	// TODO: i still hate myself. why am i here? just to suffer?
	private static DataAccess.Data.TourPlannerContext tourPlannerContext = new();
	private static IRepository<DataAccess.Models.TourLog> tourLogRepository = new TourLogRepository(tourPlannerContext);
	private static IService<TourLog> tourLogService = new TourLogService(tourLogRepository);

	public TourLogModel()
	{
		TourLogs = tourLogService.GetAll().ToList();
	}

	public TourLogModel(int id)
	{
		TourLogs = [.. tourLogService.GetLogsByTourId(id)];
	}

	public static void CreateTourLog(int tourId, DateTime date, string comment, int difficulty, float distance, double time, int rating)
	{
		TourLog tourLog = new()
		{
			Date = date,
			Comment = comment,
			Difficulty = difficulty,
			Distance = distance,
			Time = time,
			Rating = rating,
			TourId = tourId
		};

		_ = tourLogService.Create(tourLog);
	}

	public static void UpdateTourLog(TourLogModel model)
	{
		if (model is null)
		{
			return;
		}

		TourLog temp = new()
		{
			LogId = model.LogId,
			Date = model.Date,
			Comment = model.Comment,
			Difficulty = model.Difficulty,
			Distance = model.Distance,
			Time = model.Time,
			Rating = model.Rating,
			TourId = model.TourId
		};

		_ = tourLogService.Update(temp);
	}

	public static TourLogModel? GetTourLog(int id)
	{
		TourLog? temp = tourLogService.GetById(id);

		if (temp == null)
		{
			return null;
		}

		TourLogModel logModel = new()
		{
			LogId = id,
			Date = temp.Date,
			Comment = temp.Comment,
			Difficulty = temp.Difficulty,
			Distance = temp.Distance,
			Time = temp.Time,
			Rating = temp.Rating,
			TourId = temp.TourId
		};

		return logModel;
	}

	public void DeleteLog(int tourId)
	{
		tourLogService.Delete(tourId);
	}
}
