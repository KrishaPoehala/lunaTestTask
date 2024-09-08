using AutoMapper;
using LunaTestTask.Application.Common.Dtos;
using LunaTestTask.Application.Common.Exceptions;
using LunaTestTask.Application.Common.Interfaces;
using LunaTestTask.Domain.Repositories;
using MediatR;

namespace LunaTestTask.Application.User.Queries.GetTaskByIdQuery;

public record GetTaskByIdQuery(Guid Id) 
    : IRequest<TaskDto>;

public class GetTaskByIdQueryHandler(ITaskRepository taskRepository, IMapper mapper,
    ICurrentUserService currentUserService) 
    : IRequestHandler<GetTaskByIdQuery, TaskDto>
{
    public async Task<TaskDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var taskEntity = await taskRepository.GetById(request.Id, cancellationToken);
        if(taskEntity is null)
        {
            throw new NotFoundException(nameof(taskEntity), request.Id);
        }

        if(taskEntity.UserId != currentUserService.Id)
        {
            throw new ForbiddenException(taskEntity.Id);
        }

        var taskDto = mapper.Map<TaskDto>(taskEntity);
        return taskDto;
    }
}