
namespace LunaTestTask.Application.Common.Dtos;

public class AuthResponseDto
{
    public string? AccessToken { get; set; }
    public IEnumerable<string>? AuthErrors { get; set; }
    public bool IsSuccessfull { get; set; }
    public AuthResponseDto(string accessToken)
    {
        AccessToken = accessToken;
        AuthErrors = null;
        IsSuccessfull = true;
    }

    public AuthResponseDto(IEnumerable<string> errors)
    {
        AuthErrors = errors;
        AccessToken = null;
        IsSuccessfull = false;
    }
}
