using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Mappers.Interfaces;

namespace Sellius.API.Application.Mappers;

public sealed class AuthenticationMapper : IAuthenticationMapper
{
    public LoginRegister DtoUserRegisterToMain(UserRegister dto, Guid userId) =>
        new LoginRegister
        {
            Email = dto.Email,
            Password = dto.Password,
            UserId = userId
        };
}
