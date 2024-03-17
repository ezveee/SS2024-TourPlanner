using Npgsql;

namespace DataAccess.Interfaces;
internal interface IDbConnection
{
	void OpenConnection();
	void CloseConnection();
	void ExecuteNonQueryFromFile(string filePath, NpgsqlParameter[]? parameters = null);

}
