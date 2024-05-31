namespace Business.Models;

public class TourLog
{
	public int LogId { get; set; }
	public DateTime Date { get; set; }
	public string Comment { get; set; } = null!;
	public int Difficulty { get; set; }
	public float Distance { get; set; }
	public double Time { get; set; }
	public int Rating { get; set; }
	public int TourId { get; set; }
}