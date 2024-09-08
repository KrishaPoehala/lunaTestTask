using LunaTestTask.Application.Common.Exceptions;
using LunaTestTask.Application.Common.Interfaces;
using LunaTestTask.Domain.Entities;
using LunaTestTask.Domain.Repositories;
using MediatR;

namespace LunaTestTask.Application.User.Commands.EditTaskCommand;

public record EditTaskCommand(Guid Id, string Title, string? Description,
    DateTime? DueDate, TaskEntityStatus Status, TaskEnitityPriority Priority)
    : IRequest<Guid>;
public class EditTaskCommandHandler(ITaskRepository taskRepository,IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) 
    : IRequestHandler<EditTaskCommand, Guid>
{
    public async Task<Guid> Handle(EditTaskCommand request, CancellationToken cancellationToken)
    {
        var taskToEdit = await taskRepository.GetById(request.Id, cancellationToken);
        if(taskToEdit is null)
        {
            throw new NotFoundException(nameof(taskToEdit), request.Id);
        }

        if(taskToEdit.UserId != currentUserService.Id)
        {
            throw new ForbiddenException(taskToEdit.Id);
        }

        taskToEdit.Title = request.Title;
        taskToEdit.Description = request.Description;
        taskToEdit.DueDate = request.DueDate;
        taskToEdit.Status = request.Status;
        taskToEdit.Priority = request.Priority;
        taskToEdit.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return request.Id;
    }
}