using LunaTestTask.Application.Common.Interfaces;
using LunaTestTask.Domain.Entities;
using LunaTestTask.Domain.Repositories;
using LunaTestTask.Domain.RepositoryParameters;
using LunaTestTask.Infrastructure.Extentions;
using LunaTestTask.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace LunaTestTask.Infrastructure.Repositories;

public class TaskRepository(ApplicationDbContext context,ICurrentUserService currentUserService) 
    : ITaskRepository
{
    public void Add(TaskEntity newTask) => context.Set<TaskEntity>().Add(newTask);

    public async Task<IEnumerable<TaskEntity>> GetAll(GetAllTasksOptions parameters, CancellationToken token)
    {
        var query = context
            .Set<TaskEntity>()
            .Include(x => x.User)
            .AsQueryable();
        if (parameters.PriorityFilter is not null)
        {
            query = query.Where(x => x.Priority == parameters.PriorityFilter);
        }

        if(parameters.DueDateFilter is not null)
        {
            query = query.Where(x => x.DueDate == parameters.DueDateFilter);
        }

        if(parameters.StatusFilter is not null)
        {
            query = query.Where(x => x.Status == parameters.StatusFilter);
        }

        if(parameters.SortingOption == SortingOption.Priority)
        {
            query = query.OrderBy(x => x.Priority, parameters.SortingOrder);
        }

        if(parameters.SortingOption == SortingOption.DueDate)
        {
            query = query.OrderBy(x => x.DueDate, parameters.SortingOrder);
        }

        query = query.Where(x => x.UserId == currentUserService.Id);
        query = query
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize);

        return await query.ToListAsync(token);
    }

    public Task<TaskEntity?> GetById(Guid id, CancellationToken token)
    {
        var task = context.Set<TaskEntity>().AsQueryable();
        return task.FirstOrDefaultAsync(x => x.Id == id, token);
    }

    public void Remove(TaskEntity taskToRemove) => context.Set<TaskEntity>().Remove(taskToRemove);
}