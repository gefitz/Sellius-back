using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.MenuServices.CommandServices.Interfaces;
using Sellius.API.Infra.Repository.Users.Interfaces;

namespace Sellius.API.Application.Services.MenuServices.CommandServices;

public sealed class MenuCommandService(
    IMenuRepository repository,
    IMenuMapper mapper) : IMenuCommandService
{
    public async Task<bool> CreateMenu(MenuRegister dto, Guid? enterpriseId)
    {
        if (await MenuAlreadyExists(dto.Name, enterpriseId))
            return false;

        var menu = mapper.DtoRegisterToMain(dto);
        menu.EnterpriseId = enterpriseId;
        menu.CreateDate = DateTime.UtcNow;
        menu.AlteredDate = DateTime.UtcNow;
        menu.Active = 1;

        return await repository.CreateMenuAsync(menu);
    }

    public async Task<bool> UpdateMenu(MenuRegister dto)
    {
        var menu = await repository.FindByPredicateAsync(
            m => m.Id == dto.Id);

        if (menu is null)
            return false;

        menu.DescMenu = dto.Name;
        menu.UrlMenu = dto.UrlMenu ?? menu.UrlMenu;
        menu.Icon = dto.Icon;
        menu.MenuFatherId = (int?)dto.MenuFatherId;
        menu.Active = dto.Active;
        menu.AlteredDate = DateTime.UtcNow;

        return await repository.UpdateMenuAsync(menu);
    }

    public async Task<bool> InactiveMenu(long menuId)
    {
        var menu = await repository.FindByPredicateAsync(
            m => m.Id == menuId);

        if (menu is null)
            return false;

        menu.Active = 0;
        menu.AlteredDate = DateTime.UtcNow;

        return await repository.UpdateMenuAsync(menu);
    }

    private async Task<bool> MenuAlreadyExists(string name, Guid? enterpriseId)
    {
        var existing = await repository.FindByPredicateAsync(
            m => m.DescMenu == name && m.EnterpriseId == enterpriseId);

        return existing is not null;
    }
}
