using LunaTestTask.Domain.Repositories;
using LunaTestTask.Infrastructure.Persistance;

namespace LunaTestTask.Infrastructure.UnitOfWork;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) => 
        await context.SaveChangesAsync(cancellationToken);
}
