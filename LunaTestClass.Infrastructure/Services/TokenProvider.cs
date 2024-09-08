using LunaTestTask.Application.Common.Interfaces;
using LunaTestTask.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LunaTestClass.Infrastructure.Services;

public class TokenProvider(IConfiguration configuration) : ITokenProvider
{
    private readonly IConfiguration _configuration = configuration;

    public string GenerateToken(User user)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));
        var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);
        var options = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims:
            [
                new Claim("id", user.Id.ToString()),
            ],
            expires: DateTime.Now.AddMinutes(double.Parse(_configuration["JwtSettings:ExpiryTime"])),
            signingCredentials: signInCredentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(options);
        return token;
    }
}