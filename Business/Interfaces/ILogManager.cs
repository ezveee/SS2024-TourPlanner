using Business.Models;

namespace Business.Interfaces;
internal interface ILogManager
{
	void CreateLog(TourLog log);
	void UpdateLog(TourLog modifiedLog);
	List<TourLog> GetLogs();
	void DeleteLog(int logId);
}
