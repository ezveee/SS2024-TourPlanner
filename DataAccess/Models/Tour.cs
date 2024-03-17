using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;
public class Tour
{
	[Key] public int TourId { get; set; }
	public string Name { get; set; } = null!;
	public string Description { get; set; } = null!;
	public string From { get; set; } = null!;
	public string To { get; set; } = null!;
	public string TransportType { get; set; } = null!;
	public float Distance { get; set; }
	public double EstimatedTime { get; set; }
	public string RouteInformation { get; set; } = null!;
	public ICollection<TourLog> Logs { get; set; } = null!;
}
