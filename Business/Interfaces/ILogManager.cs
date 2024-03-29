using DataAccess.Models;

namespace Business.Interfaces;
internal interface ILogManager
{
	void CreateLog(DateTime date, string comment,
		int difficulty, float distance,
		double time, int rating, int tourId); // TODO: create struct for log details
	void UpdateLog(int logId);
	List<Tour> GetLogs(); // TODO: change Tour - it's a DAL Model, shouldn't be used in UI
	void DeleteLog(int logId);
}
