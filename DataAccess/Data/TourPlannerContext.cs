using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data;
internal class TourPlannerContext : DbContext
{
	public DbSet<Tour> Tours { get; set; } = null!;
	public DbSet<TourLog> TourLogs { get; set; } = null!;

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		_ = optionsBuilder.UseNpgsql("Host=localhost;Database=TourPlanner;Username=swen2;Password=123"); // TODO: secure connection string
	}
}
