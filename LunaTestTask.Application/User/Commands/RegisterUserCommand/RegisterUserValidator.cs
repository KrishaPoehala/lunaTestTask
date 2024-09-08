using FluentValidation;

namespace LunaTestTask.Application.User.Commands.RegisterUserCommand;

public class RegisterUserValidator :AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(command => command.Password)
           .NotEmpty()
           .MinimumLength(8)
           .Matches(@"[A-Z]") // at least one uppercase letter
           .Matches(@"[a-z]") // at least one lowercase letter
           .Matches(@"[0-9]") // at least one digit
           .Matches(@"[^\w\d]") // at least one special character
           .WithMessage("Password must be at least 8 characters long and" +
           "contain at least one uppercase letter, one lowercase letter," +
           "one digit, and one special character.");
    }
}
