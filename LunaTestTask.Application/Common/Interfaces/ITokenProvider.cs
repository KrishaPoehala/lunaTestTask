namespace LunaTestTask.Application.Common.Interfaces;

public interface ITokenProvider
{
    string GenerateToken(Domain.Entities.User user);
}