using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.MenuServices.QueryServices.Interfaces;
using Sellius.API.Infra.Repository.Users.Interfaces;

namespace Sellius.API.Application.Services.MenuServices.QueryServices;

public sealed class MenuQueryService(
    IMenuRepository repository,
    IMenuMapper mapper) : IMenuQueryService
{
    public async Task<MenuEdit> FindByMenuId(long menuId)
    {
        var menu = await repository.FindByPredicateAsync(
            m => m.Id == menuId);

        return menu is not null ? mapper.MainToDtoEdit(menu) : new MenuEdit();
    }

    public async Task<List<MenuRegister>> FindAllMenus(MenuFilter filter, Guid? enterpriseId)
    {
        var menus = await repository.FindAllAsync(
            m => (enterpriseId == null || m.EnterpriseId == enterpriseId)
                 && (filter.Menu == null || m.DescMenu.Contains(filter.Menu))
                 && (filter.Active < 0 || m.Active == filter.Active),
            null,
            o => o.OrderBy(m => m.DescMenu));

        return mapper.MainToList(menus);
    }
}
