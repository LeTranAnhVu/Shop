namespace Shop.Application.Common.Exceptions;

public class BadRequest: Exception
{
    public BadRequest(string message): base(message)
    {
    } 
}