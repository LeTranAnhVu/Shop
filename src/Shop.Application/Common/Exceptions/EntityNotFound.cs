namespace Shop.Application.Common.Exceptions;

public class EntityNotFound: Exception
{
    public EntityNotFound(string entityName): base($"{entityName} not found")
    {
    } 
}