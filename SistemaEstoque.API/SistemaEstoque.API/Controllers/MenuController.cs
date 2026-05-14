using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Services.MenuServices.CommandServices.Interfaces;
using Sellius.API.Application.Services.MenuServices.QueryServices.Interfaces;
using Sellius.API.Domain.Extensions;

namespace Sellius.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class MenuController(
    IMenuCommandService commandService,
    IMenuQueryService queryService) : Controller
{
    [HttpPost]
    [Authorize(Policy = "podeGerenciarUsuarios")]
    public async Task<IActionResult> Create(MenuRegister dto)
    {
        var result = await commandService.CreateMenu(dto, User.GetEnterpriseId());
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to create menu" });
    }

    [HttpPut]
    [Authorize(Policy = "podeGerenciarUsuarios")]
    public async Task<IActionResult> Update(MenuRegister dto)
    {
        var result = await commandService.UpdateMenu(dto);
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to update menu" });
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "podeGerenciarUsuarios")]
    public async Task<IActionResult> Inactivate(long id)
    {
        var result = await commandService.InactiveMenu(id);
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to inactivate menu" });
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await queryService.FindByMenuId(id);
        return Ok(result);
    }

    [HttpPost("list")]
    public async Task<IActionResult> GetAll(MenuFilter filter)
    {
        var result = await queryService.FindAllMenus(filter, User.GetEnterpriseId());
        return Ok(result);
    }
}
