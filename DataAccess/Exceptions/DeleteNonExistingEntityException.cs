namespace DataAccess.Exceptions;
internal class DeleteNonExistingEntityException : Exception
{
	public DeleteNonExistingEntityException()
	{
	}

	public DeleteNonExistingEntityException(string message)
		: base(message)
	{
	}

	public DeleteNonExistingEntityException(string message, Exception inner)
		: base(message, inner)
	{
	}
}
