using LunaTestClass.Infrastructure.Services;
using LunaTestTask.Application.Common.Interfaces;
using LunaTestTask.Domain.Repositories;
using LunaTestTask.Infrastructure.Persistance;
using LunaTestTask.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LunaTestTask.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection self, IConfiguration configuration)
    {
        var b = configuration.GetConnectionString("DefaultConnection");
        self.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        self.AddScoped<ITokenProvider, TokenProvider>();
        self.AddScoped<IUserRepository, UserRepository>();
        self.AddScoped<ITaskRepository, TaskRepository>();
        self.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        self.AddScoped<ApplicationDbContextInitializer>();
            
        self.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                var g = configuration["JwtSettings:Secret"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(configuration["JwtSettings:Secret"])),
                };

            });
        return self;
    }
}