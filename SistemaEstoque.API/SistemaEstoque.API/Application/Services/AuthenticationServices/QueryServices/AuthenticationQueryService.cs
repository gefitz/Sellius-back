using Microsoft.EntityFrameworkCore;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Services.AuthenticationServices.QueryServices.Interfaces;
using Sellius.API.Application.Services.TokenServices.Interfaces;
using Sellius.API.Domain.Entity.EntityUsers;
using Sellius.API.Domain.Extensions;
using Sellius.API.Infra.Repository.Users.Interfaces;

namespace Sellius.API.Application.Services.AuthenticationServices.QueryServices;

public sealed class AuthenticationQueryService(
    ILoginRepository repository,
    ITokenService tokenService) : IAuthenticationQueryService
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

        return authentication.Hash.Verify(login.Password);
    }

    public async Task<string?> Login(LoginRegister login)
    {
        var authentication = await repository.FindByPredicateAsync(
            a => a.Email == login.Email,
            q => q.Include(a => a.User)
                  .ThenInclude(u => u!.TypeUser)
                  .ThenInclude(t => t!.UserConfiguration)!,
            true);

        if (authentication is null)
            return null;

        if (!authentication.Hash.Verify(login.Password))
            return null;

        return tokenService.GenerateToken(authentication);
    }
}
