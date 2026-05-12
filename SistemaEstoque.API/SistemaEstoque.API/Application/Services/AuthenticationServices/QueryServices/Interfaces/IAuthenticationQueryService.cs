using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Domain.Entity.EntityUsers;

namespace Sellius.API.Application.Services.AuthenticationServices.QueryServices.Interfaces;

public interface IAuthenticationQueryService
{
    Task<Authentication?> FindByEmail(string email);
    Task<bool> ValidateCredentials(LoginRegister login);
}
