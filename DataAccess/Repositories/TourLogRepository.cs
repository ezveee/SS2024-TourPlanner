using DataAccess.Data;
using DataAccess.Models;

namespace DataAccess.Repositories;

public class TourLogRepository(TourPlannerContext context) : GenericRepository<TourLog>(context)
{
	public override IEnumerable<TourLog> GetAll()
	{
		return context.TourLogs.OrderBy(log => log.LogId);
	}

	//public void Update(TourLog entity)
	//{
	//	TourLog? existingEntity = context.TourLogs.Find(entity.LogId);
	//	if (existingEntity is not null)
	//	{
	//		context.Entry(existingEntity).CurrentValues.SetValues(entity);
	//		_ = context.SaveChanges();
	//	}
	//}
}