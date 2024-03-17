using Npgsql;

namespace DataAccess.Interfaces;
public interface IDbConnection
{
	void OpenConnection();
	void CloseConnection();
	void ExecuteNonQueryFromFile(string filePath, NpgsqlParameter[]? parameters = null);

}
