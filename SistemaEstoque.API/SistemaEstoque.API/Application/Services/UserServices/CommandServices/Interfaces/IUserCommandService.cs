using Sellius.API.Application.DTOs.RegisterDTOs;

namespace Sellius.API.Services.UserServices.CommandServices.Interfaces;

public interface IUserCommandService
{
    Task<bool> CreateUser(UserRegister dtoRegister);
    Task<bool> UpdateUser(UserRegister dtoRegister);
    Task<bool> InactiveUser(Guid userId);
}