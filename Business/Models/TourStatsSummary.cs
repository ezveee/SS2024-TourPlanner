using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models;
public class TourStatsSummary
{
	public string Tourname { get; set; } = null!;
	public float AverageDistance { get; set; }
	public double AverageTime { get; set; }
	public int AverageRating { get; set; }
}
