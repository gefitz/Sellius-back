using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Domain.Entity.EntityUsers;

namespace Sellius.API.Application.Mappers;

public sealed class MenuMapper : IMenuMapper
{
    public Menu DtoRegisterToMain(MenuRegister dto) =>
        new Menu
        {
            DescMenu = dto.Name,
            UrlMenu = dto.UrlMenu ?? string.Empty,
            Icon = dto.Icon,
            MenuFatherId = (int?)dto.MenuFatherId,
            Active = dto.Active,
            CreateDate = dto.DateCreate,
            AlteredDate = dto.DateAltered
        };

    public MenuEdit MainToDtoEdit(Menu menu) =>
        new MenuEdit
        {
            Id = menu.Id,
            Name = menu.DescMenu,
            UrlMenu = menu.UrlMenu,
            Icon = menu.Icon,
            MenuFatherId = menu.MenuFatherId,
            EnterpriseId = menu.EnterpriseId,
            Active = menu.Active,
            CreateDate = menu.CreateDate,
            AlteredDate = menu.AlteredDate
        };

    public List<MenuRegister> MainToList(List<Menu> menus) =>
        menus.Select(m => new MenuRegister
        {
            Id = m.Id,
            Name = m.DescMenu,
            UrlMenu = m.UrlMenu,
            Icon = m.Icon,
            MenuFatherId = m.MenuFatherId,
            Active = m.Active,
            DateCreate = m.CreateDate,
            DateAltered = m.AlteredDate
        }).ToList();
}
