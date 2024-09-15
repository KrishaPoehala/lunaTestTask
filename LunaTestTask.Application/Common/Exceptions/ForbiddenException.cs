namespace LunaTestTask.Application.Common.Exceptions;

/// <summary>
/// Is thrown when a user does not own tasks its trying to access
/// </summary>
public class ForbiddenException : ApplicationException
{
    public ForbiddenException(Guid taskId) 
        : base($"You do not have permission to view task with id: {taskId}")
    {}
}
