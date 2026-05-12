using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.AuthenticationServices.CommandServices.Interfaces;
using Sellius.API.Domain.Entity.EntityUsers;
using Sellius.API.Infra.Repository.Users.Interfaces;
using Sellius.API.Services.UserServices.CommandServices.Interfaces;

namespace Sellius.API.Application.Services.UserServices.CommandServices;

public class UserCommandService(
    IUserRepository repository,
    IUserMapper mapper,
    IAuthenticationCommandServices authenticationCommandServices,
    IAuthenticationMapper mapperAuthentication) 
    : IUserCommandService
{
    public async Task<bool> CreateUser(UserRegister dtoRegister)
    {
        var user = mapper.DtoRegisterToMain(dtoRegister);
        
        if(!await ValidUser(user))
            return false;
        
        if(!await repository.CreateUserAsync(user))
            return false;
        
        var login = mapperAuthentication.DtoUserRegisterToMain(dtoRegister,user.Id);
        
        return await authenticationCommandServices.CreateLogin(login);
    }

    public async Task<bool> UpdateUser(UserRegister dtoRegister)
    {
        var user = mapper.DtoRegisterToMain(dtoRegister);
        var response = await repository.UpdateUserAsync(user);
        return response;
    }

    public async Task<bool> InactiveUser(Guid userId)
    {
        var user = await repository.FindPredicateUserAsync(u => u.Id == userId);

        if (user is null)
            return false;
        
        user.Active = 0;
        user.AlteredDate = DateTime.UtcNow;
        
        return await repository.UpdateUserAsync(user);
    }

    private async Task<bool> ValidUser(User user)
    {
        if (await repository.ExistsUser(u => u.Email == user.Email))
            return false;

        if (await repository.ExistsUser(u => u.Document == user.Document))
            return false;

        return true;
    }
}