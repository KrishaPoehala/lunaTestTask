using FluentValidation;
using LunaTestTask.Application.Common.Behaviors;
using LunaTestTask.Application.User.Commands.RegisterUserCommand;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LunaTestTask.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection self)
    {
        self.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        self.AddAutoMapper(Assembly.GetExecutingAssembly());
        self.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        self.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();
        return self;
    }
}