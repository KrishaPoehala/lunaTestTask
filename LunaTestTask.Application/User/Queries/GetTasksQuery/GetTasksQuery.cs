using AutoMapper;
using LunaTestTask.Application.Common.Dtos;
using LunaTestTask.Domain.Entities;
using LunaTestTask.Domain.Repositories;
using LunaTestTask.Domain.RepositoryParameters;
using MediatR;

namespace LunaTestTask.Application.User.Queries.GetTasksQuery;

public record GetTasksQuery(string? Status,
    string? DueDate, string? Priority,
    string? SortBy, bool SortingOrder = false,
    int PageNumber = 1,int PageSize = 5) 
    : IRequest<IEnumerable<TaskDto>>;

public class GetTaskQueryHandler(ITaskRepository taskRepository, IMapper mapper) 
    : IRequestHandler<GetTasksQuery, IEnumerable<TaskDto>>
{
    public async Task<IEnumerable<TaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        GetAllTasksOptions getAllTasksOptions = ParseRequestToTasksOptions(request);
        var allTasks = await taskRepository.GetAll(getAllTasksOptions, cancellationToken);
        return allTasks.Select(mapper.Map<TaskDto>);
    }

    //parsing provided values(strings) to corresponding types
    private static GetAllTasksOptions ParseRequestToTasksOptions(GetTasksQuery request)
    {
        TaskEntityStatus? statusFilter = null;
        if (request.Status is not null)
        {
            statusFilter = (TaskEntityStatus)Enum.Parse(typeof(TaskEntityStatus),
                request.Status, ignoreCase: true);
        }

        DateTime? dueDateTimeFilter = null;
        if (request.DueDate is not null)
        {
            dueDateTimeFilter = DateTime.Parse(request.DueDate);
        }

        TaskEnitityPriority? priorityFiler = null;
        if (request.Priority is not null)
        {
            priorityFiler = (TaskEnitityPriority)Enum.Parse(typeof(TaskEnitityPriority),
                request.Priority, ignoreCase: true);
        }

        SortingOption? sortingOption = null;
        if (request.SortBy is not null)
        {
            sortingOption = (SortingOption)Enum.Parse(typeof(SortingOption),
                request.SortBy,ignoreCase:true);
        }

        var getAllTasksContext = new GetAllTasksOptions(statusFilter, dueDateTimeFilter,
            priorityFiler, sortingOption, request.SortingOrder,request.PageSize,request.PageNumber);

        return getAllTasksContext;
    }
}