namespace LunaTestTask.Application.Common.Dtos;

public class AuthResponseDto(string accessToken)
{
    public string? AccessToken { get; set; } = accessToken;
}