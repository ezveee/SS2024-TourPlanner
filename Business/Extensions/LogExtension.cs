using Business.Models;

namespace Business.Extensions;
public static class LogExtension
{
	public static DataAccess.Models.TourLog MapLogToDataAccess(this TourLog log) // extension method
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

	public static TourLog MapLogToBusiness(this DataAccess.Models.TourLog log)
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
