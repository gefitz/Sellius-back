using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Domain.Entity.EntityUsers;

namespace Sellius.API.Application.Mappers.Interfaces;

public interface IAuthenticationMapper
{
    LoginRegister DtoUserRegisterToMain(UserRegister dto,Guid userId);
}