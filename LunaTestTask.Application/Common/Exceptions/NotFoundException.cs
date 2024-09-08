namespace LunaTestTask.Application.Common.Exceptions;

/// <summary>
/// Is thrown when an entity was not found in the databse
/// </summary>
public class NotFoundException :ApplicationException
{
    public NotFoundException(string name, Guid id)
        : base($"{name} was not found with id: {id}")
    {}

    public NotFoundException(string email) 
        : base($"User was not found with an email: {email}")
    {}
}
