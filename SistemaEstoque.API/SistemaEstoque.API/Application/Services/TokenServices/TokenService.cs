using Microsoft.IdentityModel.Tokens;
using Sellius.API.Application.Services.TokenServices.Interfaces;
using Sellius.API.Domain.Entity.EntityUsers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Sellius.API.Application.Services.TokenServices;

public sealed class TokenService(IConfiguration configuration) : ITokenService
{
    public string? GenerateToken(Authentication authentication)
    {
        try
        {
            var configJson = JsonSerializer.Serialize(authentication.User!.TypeUser!.UserConfiguration);

            Claim[] claims =
            [
                new Claim("id", authentication.UserId.ToString()),
                new Claim("user", authentication.User.Name),
                new Claim(ClaimTypes.Role, authentication.User.TypeUserId.ToString()),
                new Claim("empresa", authentication.EnterpriseId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("config", configJson, JsonClaimValueTypes.Json)
            ];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:secretkey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["jwt:issuer"],
                audience: configuration["jwt:audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        catch
        {
            return null;
        }
    }
}
