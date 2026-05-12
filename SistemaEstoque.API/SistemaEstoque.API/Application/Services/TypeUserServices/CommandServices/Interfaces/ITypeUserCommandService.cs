using Sellius.API.Application.DTOs.RegisterDTOs;

namespace Sellius.API.Application.Services.TypeUserServices.CommandServices.Interfaces;

public interface ITypeUserCommandService
{
    Task<bool> CreateTypeUser(TypeUserRegister dto);
    Task<bool> UpdateTypeUser(TypeUserRegister dto);
    Task<bool> InactiveTypeUser(long typeUserId);
}
