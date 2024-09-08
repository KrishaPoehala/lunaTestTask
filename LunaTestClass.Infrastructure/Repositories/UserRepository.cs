using LunaTestClass.Infrastructure.Persistance;
using LunaTestTask.Domain.Entities;
using LunaTestTask.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LunaTestTask.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public void Add(User user) => context.Set<User>().Add(user);

    public async Task<bool> ExistsByEmail(string email, CancellationToken cancellationToken) => 
         await context
        .Set<User>()
        .AnyAsync(x => x.Email == email, cancellationToken);    

    public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken) => 
        await context
        .Set<User>()
        .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
}