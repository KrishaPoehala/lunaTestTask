using LunaTestTask.Domain.Entities;
using LunaTestTask.Domain.RepositoryParameters;

namespace LunaTestTask.Domain.Repositories;

public interface ITaskRepository
{
    Task<IEnumerable<TaskEntity>> GetAll(GetAllTasksOptions parameters, CancellationToken token);
    Task<TaskEntity?> GetById(Guid id,CancellationToken token);
    void Add(TaskEntity newTask);
    void Remove(TaskEntity taskToRemove);
}