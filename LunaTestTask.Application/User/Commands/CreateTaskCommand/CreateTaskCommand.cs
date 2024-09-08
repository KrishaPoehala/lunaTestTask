using AutoMapper;
using LunaTestTask.Application.Common.Dtos;
using LunaTestTask.Application.Common.Interfaces;
using LunaTestTask.Domain.Entities;
using LunaTestTask.Domain.Repositories;
using MediatR;

namespace LunaTestTask.Application.User.Commands.CreateTaskCommand;

public record CreateTaskCommand(string Title, string? Description,
    DateTime? DueDate, TaskEntityStatus Status,
    TaskEnitityPriority Priority) 
    : IRequest<TaskDto>;

public class CreateTaskCommandHandler(ITaskRepository taskRepository,
    IUnitOfWork unitOfWork, IMapper mapper,ICurrentUserService currentUserService) 
    : IRequestHandler<CreateTaskCommand, TaskDto>
{
    public async Task<TaskDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var newTask = mapper.Map<TaskEntity>(request);
        newTask.UserId = currentUserService.Id;
        taskRepository.Add(newTask);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<TaskDto>(newTask);
    }
}