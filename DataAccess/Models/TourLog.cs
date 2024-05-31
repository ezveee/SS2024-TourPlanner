using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models;

[Table("tourlogs")]
public class TourLog
{
	[Key][Column("logid")] public int LogId { get; set; }
	[Column("date")] public DateTime Date { get; set; }
	[Column("comment")] public string Comment { get; set; } = null!;
	[Column("difficulty")] public int Difficulty { get; set; }
	[Column("distance")] public float Distance { get; set; }
	[Column("time")] public double Time { get; set; }
	[Column("rating")] public int Rating { get; set; }

	[Column("tourid")] public int TourId { get; set; }
}