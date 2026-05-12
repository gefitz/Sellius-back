using Sellius.API.Application.DTOs.RegisterDTOs;

namespace Sellius.API.Application.Services.MenuServices.CommandServices.Interfaces;

public interface IMenuCommandService
{
    Task<bool> CreateMenu(MenuRegister dto, Guid? enterpriseId);
    Task<bool> UpdateMenu(MenuRegister dto);
    Task<bool> InactiveMenu(long menuId);
}
