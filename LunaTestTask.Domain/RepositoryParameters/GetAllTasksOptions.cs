using LunaTestTask.Domain.Entities;

namespace LunaTestTask.Domain.RepositoryParameters;

public record GetAllTasksOptions(TaskEntityStatus? StatusFilter,
    DateTime? DueDateFilter, TaskEnitityPriority? PriorityFilter,
    SortingOption? SortingOption, bool SortingOrder,
    int PageSize,int PageNumber);