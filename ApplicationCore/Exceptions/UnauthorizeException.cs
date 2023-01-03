namespace ApplicationCore.Exceptions;

public class UnauthorizeException : Exception
{
    public UnauthorizeException(string message) : base(message)
    {
    }
}