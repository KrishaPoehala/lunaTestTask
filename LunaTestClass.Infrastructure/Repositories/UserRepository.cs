﻿using LunaTestTask.Domain.Entities;
using LunaTestTask.Domain.Repositories;
using LunaTestTask.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace LunaTestTask.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public void Add(User user) => context.Set<User>().Add(user);

    public async Task<bool> ExistsByEmail(string email, CancellationToken cancellationToken) => 
         await context
        .Set<User>()
        .AnyAsync(x => x.Email == email, cancellationToken);

    public async Task<bool> ExistsByName(string username, CancellationToken cancellationToken) => 
         await context
        .Set<User>()
        .AnyAsync(x => x.Username == username, cancellationToken);

    public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken) => 
        await context
        .Set<User>()
        .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
}