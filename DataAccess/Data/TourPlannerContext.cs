using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Data;
public class TourPlannerContext : DbContext
{
	public DbSet<Tour> Tours { get; set; } = null!;
	public DbSet<TourLog> TourLogs { get; set; } = null!;

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		IConfiguration config = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("config.json", false, true)
			.Build();

		_ = optionsBuilder.UseNpgsql(config["dbConnectionString"]); // TODO: secure connection string
	}
}
