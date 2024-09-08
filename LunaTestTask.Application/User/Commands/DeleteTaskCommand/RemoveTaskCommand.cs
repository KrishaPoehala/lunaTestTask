using LunaTestTask.Application.Common.Exceptions;
using LunaTestTask.Application.Common.Interfaces;
using LunaTestTask.Domain.Repositories;
using MediatR;

namespace LunaTestTask.Application.User.Commands.DeleteTaskCommand;

public record RemoveTaskCommand(Guid Id) : IRequest;

public class RemoveTaskCommandHandler(ITaskRepository taskRepository, IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) 
    : IRequestHandler<RemoveTaskCommand>
{
    public async Task Handle(RemoveTaskCommand request, CancellationToken cancellationToken)
    {
        var taskToRemove = await taskRepository.GetById(request.Id, cancellationToken);
        if(taskToRemove is null)
        {
            throw new NotFoundException(nameof(taskToRemove), request.Id);
        }

        if(currentUserService.Id != taskToRemove.UserId)
        {
            throw new ForbiddenException(taskToRemove.Id);
        }

        taskRepository.Remove(taskToRemove);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

