using DataAccess.Interfaces;
using Npgsql;

namespace DataAccess;
public class DbConnection(string connectionString) : IDbConnection
{
	private readonly NpgsqlConnection _connection = new(connectionString);

	public void OpenConnection()
	{
		try
		{
			_connection.Open();
			Console.WriteLine("Connection opened successfully.");
		}
		catch (Exception ex) // TODO: change from default exception to (possibly) custom one
		{
			Console.WriteLine("Error opening connection: " + ex.Message);
		}
	}

	public void CloseConnection()
	{
		_connection.Close();
		Console.WriteLine("Connection closed successfully.");
	}

	public void ExecuteNonQueryFromFile(string filePath, NpgsqlParameter[]? parameters = null)
	{
		string query = File.ReadAllText(filePath);

		try
		{
			using NpgsqlCommand cmd = new(query, _connection);

			if (parameters != null)
			{
				cmd.Parameters.AddRange(parameters);
			}
			_ = cmd.ExecuteNonQuery();
		}
		catch (Exception ex) // TODO: change from default exception to (possibly) custom one
		{
			Console.WriteLine("Error executing query: " + ex.Message);
		}
	}
}
