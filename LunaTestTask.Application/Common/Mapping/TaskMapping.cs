using AutoMapper;
using LunaTestTask.Application.Common.Dtos;
using LunaTestTask.Application.User.Commands.CreateTaskCommand;
using LunaTestTask.Domain.Entities;

namespace LunaTestTask.Application.Common.Mapping;

public class TaskMapping : Profile
{
    public TaskMapping()
    {
        CreateMap<TaskEntity, TaskDto>().ReverseMap();
        CreateMap<CreateTaskCommand, TaskEntity>();
    }
}