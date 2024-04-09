using Business.Interfaces;
using Business.Managers;
using Business.Models;

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

	private readonly IManager<TourLog> manager = new LogManager();

	public TourLogModel()
	{
		TourLogs = manager.GetAll();
	}

	public TourLogModel(int id)
	{
		TourLogs = manager.GetLogsByTourId(id);
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

		IManager<TourLog> manager = new LogManager();
		manager.Create(tourLog);
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

		IManager<TourLog> manager = new LogManager();
		manager.Update(temp);
	}

	public static TourLogModel? GetTourLog(int id)
	{
		IManager<TourLog> manager = new LogManager();
		TourLog? temp = manager.GetById(id);

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
		IManager<TourLog> manager = new LogManager();
		manager.Delete(tourId);
	}
}
