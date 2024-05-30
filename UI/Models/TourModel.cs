
using System.Text.Json.Serialization;

namespace UI.Models;
public class TourModel
{
	[JsonPropertyName("tourId")]
	public int TourId { get; set; }

	[JsonPropertyName("name")]
	public string Name { get; set; } = null!;

	[JsonPropertyName("description")]
	public string Description { get; set; } = null!;

	[JsonPropertyName("from")]
	public string From { get; set; } = null!;

	[JsonPropertyName("to")]
	public string To { get; set; } = null!;

	[JsonPropertyName("transportType")]
	public string TransportType { get; set; } = null!;

	[JsonPropertyName("distance")]
	public float Distance { get; set; }

	[JsonPropertyName("estimatedTime")]
	public double EstimatedTime { get; set; }

	[JsonPropertyName("routeInformation")]
	public string RouteInformation { get; set; } = null!;
}
