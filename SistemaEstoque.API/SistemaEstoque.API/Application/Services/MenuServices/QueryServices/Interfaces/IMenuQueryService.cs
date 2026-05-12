using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.RegisterDTOs;

namespace Sellius.API.Application.Services.MenuServices.QueryServices.Interfaces;

public interface IMenuQueryService
{
    Task<MenuEdit> FindByMenuId(long menuId);
    Task<List<MenuRegister>> FindAllMenus(MenuFilter filter, Guid? enterpriseId);
}
