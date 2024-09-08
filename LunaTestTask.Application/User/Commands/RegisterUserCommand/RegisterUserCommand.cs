
using AutoMapper;
using LunaTestTask.Application.Common.Dtos;
using LunaTestTask.Application.Common.Interfaces;
using LunaTestTask.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LunaTestTask.Application.User.Commands.RegisterUserCommand;
public record RegisterUserCommand(string Username, string Email, string Password) 
    : IRequest<AuthResponseDto>;
public class RegisterUserCommandHandler(ITokenProvider tokenProvider, IMapper mapper,
    IUserRepository repository, IUnitOfWork unitOfWork) 
    : IRequestHandler<RegisterUserCommand, AuthResponseDto>
{
    public async Task<AuthResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var alreadyExists = await repository.ExistsByEmail(request.Email, cancellationToken);
        if (alreadyExists)
        {
            return new(["User with such an email alread exists"]);
        }

        var hasher = new PasswordHasher<Domain.Entities.User>();
        var user = mapper.Map<Domain.Entities.User>(request);
        user.Id = Guid.NewGuid();
        user.PasswordHash = hasher.HashPassword(user, request.Password);
        repository.Add(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        var accessToken = tokenProvider.GenerateToken(user);
        return new(accessToken);
    }
}