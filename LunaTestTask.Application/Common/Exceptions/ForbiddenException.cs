namespace LunaTestTask.Application.Common.Exceptions;

public class ForbiddenException : ApplicationException
{
    public ForbiddenException(Guid taskId) 
        : base($"You do not have permission to view task with id: {taskId}")
    {}
}
