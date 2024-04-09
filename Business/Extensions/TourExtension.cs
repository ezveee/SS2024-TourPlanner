using Business.Models;

namespace Business.Extensions;
public static class TourExtension
{
	public static DataAccess.Models.Tour MapTourToDataAccess(this Tour tour)
	{
		return new()
		{
			TourId = tour.TourId,
			Name = tour.Name,
			Description = tour.Description,
			From = tour.From,
			To = tour.To,
			TransportType = tour.TransportType,
			Distance = tour.Distance,
			EstimatedTime = tour.EstimatedTime,
			RouteInformation = tour.RouteInformation
		};
	}

	public static Tour MapTourToBusiness(this DataAccess.Models.Tour tour)
	{
		return new()
		{
			TourId = tour.TourId,
			Name = tour.Name,
			Description = tour.Description,
			From = tour.From,
			To = tour.To,
			TransportType = tour.TransportType,
			Distance = tour.Distance,
			EstimatedTime = tour.EstimatedTime,
			RouteInformation = tour.RouteInformation
		};
	}
}
