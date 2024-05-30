
using System.Text.Json.Serialization;

namespace UI.Models;
public class TourLogModel
{
	[JsonPropertyName("logId")]
	public int LogId { get; set; }

	[JsonPropertyName("tourId")]
	public int TourId { get; set; }

	[JsonPropertyName("date")]
	public DateTime Date { get; set; }

	[JsonPropertyName("comment")]
	public string Comment { get; set; } = null!;

	[JsonPropertyName("difficulty")]
	public int Difficulty { get; set; }

	[JsonPropertyName("distance")]
	public float Distance { get; set; }

	[JsonPropertyName("time")]
	public double Time { get; set; }

	[JsonPropertyName("rating")]
	public int Rating { get; set; }
}
