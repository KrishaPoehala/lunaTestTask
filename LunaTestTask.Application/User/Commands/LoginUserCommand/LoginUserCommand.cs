using LunaTestTask.Application.Common.Dtos;
using LunaTestTask.Application.Common.Interfaces;
using LunaTestTask.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LunaTestTask.Application.User.Commands.LoginUserCommand;

public record LoginUserCommand(string Email, string Password)
    : IRequest<AuthResponseDto>;

public class LoginUserCommandHandler(ITokenProvider tokenProvider, IUserRepository repository) : IRequestHandler<LoginUserCommand, AuthResponseDto>
{
    public async Task<AuthResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        //cheking if user with the specified email already exists
        var user = await repository.GetByEmail(request.Email, cancellationToken);
        if (user is null)
        {
            return new(["User with such an email does not exist"]);
        }

        //Trying to verify given password and returning a failure if an incorrect password was entered
        var hasher = new PasswordHasher<Domain.Entities.User>();
        var verificationResult = hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (verificationResult == PasswordVerificationResult.Failed)
        {
            return new(["Wrong password"]);
        }

        var token = tokenProvider.GenerateToken(user);
        return new(token);
    }
}
