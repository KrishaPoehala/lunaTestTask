using LunaTestTask.Domain.Entities;
namespace LunaTestTask.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmail(string email, CancellationToken cancellationToken);
    Task<bool> ExistsByEmail(string email, CancellationToken cancellationToken);
    void Add(User user);
}
