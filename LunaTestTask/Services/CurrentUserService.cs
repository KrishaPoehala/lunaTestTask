using LunaTestTask.Application.Common.Interfaces;

namespace LunaTestTask.Services;

public class CurrentUserService(IHttpContextAccessor? accessor) : ICurrentUserService
{
    public Guid Id => Guid.Parse(accessor.HttpContext.User.Claims.First(x => x.Type == "id").Value);
}