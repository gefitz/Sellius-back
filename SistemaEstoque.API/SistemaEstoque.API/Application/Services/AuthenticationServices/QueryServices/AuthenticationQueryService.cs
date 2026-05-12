using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Services.AuthenticationServices.QueryServices.Interfaces;
using Sellius.API.Domain.Entity.EntityUsers;
using Sellius.API.Infra.Repository.Users.Interfaces;

namespace Sellius.API.Application.Services.AuthenticationServices.QueryServices;

public sealed class AuthenticationQueryService(
    ILoginRepository repository) : IAuthenticationQueryService
{
    public async Task<Authentication?> FindByEmail(string email)
    {
        return await repository.FindByPredicateAsync(
            a => a.Email == email, null, true);
    }

    public async Task<bool> ValidateCredentials(LoginRegister login)
    {
        var authentication = await repository.FindByPredicateAsync(
            a => a.Email == login.Email, null, true);

        if (authentication is null)
            return false;

        return BCrypt.Net.BCrypt.Verify(login.Password, authentication.Hash);
    }
}
