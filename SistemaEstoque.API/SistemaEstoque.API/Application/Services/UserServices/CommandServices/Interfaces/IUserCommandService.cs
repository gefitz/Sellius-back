using Sellius.API.Application.DTOs.RegisterDTOs;

namespace Sellius.API.Application.Services.UserServices.CommandServices.Interfaces;

public interface IUserCommandService
{
    Task<bool> CreateUser(UserRegister dtoRegister,Guid enterpriseId);
    Task<bool> UpdateUser(UserRegister dtoRegister);
    Task<bool> InactiveUser(Guid userId);
}