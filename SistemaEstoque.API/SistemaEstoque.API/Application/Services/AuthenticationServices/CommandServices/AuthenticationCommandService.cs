using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Services.AuthenticationServices.CommandServices.Interfaces;
using Sellius.API.Domain.Entity.EntityUsers;
using Sellius.API.Domain.Extensions;
using Sellius.API.Infra.Repository.Users.Interfaces;

namespace Sellius.API.Application.Services.AuthenticationServices.CommandServices;

public sealed class AuthenticationCommandService(
    ILoginRepository repository,
    IUserRepository userRepository) : IAuthenticationCommandServices
{
    public async Task<bool> CreateLogin(LoginRegister register)
    {
        var user = await userRepository.FindPredicateUserAsync(
            u => u.Id == register.UserId, null, true);

        if (user is null)
            return false;

        var authentication = new Authentication
        {
            Email = register.Email,
            Hash = register.Password.Hash(),
            UserId = register.UserId,
            EnterpriseId = user.EnterpriseId
        };

        return await repository.CreateLoginAsync(authentication);
    }

    public async Task<bool> UpdatePassword(LoginRegister register)
    {
        var authentication = await repository.FindByPredicateAsync(
            a => a.UserId == register.UserId, null, false);

        if (authentication is null)
            return false;

        authentication.Hash = register.Password.Hash();

        return await repository.UpdateLoginAsync(authentication);
    }
}
