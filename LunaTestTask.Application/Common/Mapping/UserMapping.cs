using AutoMapper;
using LunaTestTask.Application.Common.Dtos;
using LunaTestTask.Application.User.Commands.RegisterUserCommand;

namespace LunaTestTask.Application.Common.Mapping;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<Domain.Entities.User, RegisterUserCommand>().ReverseMap();
        CreateMap<RegisterUserCommand, Domain.Entities.User>();
        CreateMap<Domain.Entities.User, UserDto>().ReverseMap();
    }
}
