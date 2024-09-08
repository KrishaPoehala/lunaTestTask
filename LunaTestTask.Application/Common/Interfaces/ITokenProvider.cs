namespace LunaTestTask.Application.Common.Interfaces;

/// <summary>
/// Generates a JWT access token for provided user
/// </summary>
public interface ITokenProvider
{
    string GenerateToken(Domain.Entities.User user);
}