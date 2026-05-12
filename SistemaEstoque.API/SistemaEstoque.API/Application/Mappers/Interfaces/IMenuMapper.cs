using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Domain.Entity.EntityUsers;

namespace Sellius.API.Application.Mappers.Interfaces;

public interface IMenuMapper
{
    Menu DtoRegisterToMain(MenuRegister dto);
    MenuEdit MainToDtoEdit(Menu menu);
    List<MenuRegister> MainToList(List<Menu> menus);
}
