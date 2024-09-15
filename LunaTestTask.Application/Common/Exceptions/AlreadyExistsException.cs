namespace LunaTestTask.Application.Common.Exceptions;

/// <summary>
/// Is thrown when a user with such a property does not exists
/// </summary>
public class AlreadyExistsException : ApplicationException
{
    public AlreadyExistsException(string propertyName, string propertyValue)
        : base($"User with a {propertyName} of {propertyValue} already exists")
    { }
}
