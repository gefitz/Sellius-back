using Sellius.API.Domain.Entity.EntityUsers;

namespace Sellius.API.Application.Services.TokenServices.Interfaces;

public interface ITokenService
{
    string? GenerateToken(Authentication authentication);
}
