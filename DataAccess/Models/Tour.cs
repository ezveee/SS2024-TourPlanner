using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models;

[Table("tours")]
public class Tour
{
	[Key][Column("tourid")] public int TourId { get; set; }
	[Column("name")] public string Name { get; set; } = null!;
	[Column("description")] public string Description { get; set; } = null!;
	[Column("from")] public string From { get; set; } = null!;
	[Column("to")] public string To { get; set; } = null!;
	[Column("transporttype")] public string TransportType { get; set; } = null!;
	[Column("distance")] public float Distance { get; set; }
	[Column("estimatedtime")] public double EstimatedTime { get; set; }
	[Column("routeinformation")] public string RouteInformation { get; set; } = null!;

	//public ICollection<TourLog> Logs { get; set; } = null!;
}
