using LunaTestTask.Domain.Entities;

namespace LunaTestTask.Domain.RepositoryParameters;

/// <summary>
/// A wrapper for all the filter/pagination/sorting parameters
/// </summary>
/// <param name="StatusFilter"></param>
/// <param name="DueDateFilter"></param>
/// <param name="PriorityFilter"></param>
/// <param name="SortingOption"></param>
/// <param name="SortingOrder"></param>
/// <param name="PageSize"></param>
/// <param name="PageNumber"></param>
public record GetAllTasksOptions(TaskEntityStatus? StatusFilter,
    DateTime? DueDateFilter, TaskEnitityPriority? PriorityFilter,
    SortingOption? SortingOption, bool SortingOrder,
    int PageSize,int PageNumber);