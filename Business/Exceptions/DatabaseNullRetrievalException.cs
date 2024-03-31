namespace Business.Exceptions;
public class DatabaseNullRetrievalException : Exception
{
	public DatabaseNullRetrievalException()
	{
	}

	public DatabaseNullRetrievalException(string message)
		: base(message)
	{
	}

	public DatabaseNullRetrievalException(string message, Exception inner)
		: base(message, inner)
	{
	}
}
