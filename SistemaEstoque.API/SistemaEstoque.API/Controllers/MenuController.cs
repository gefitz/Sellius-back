using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Services.MenuServices.CommandServices.Interfaces;
using Sellius.API.Application.Services.MenuServices.QueryServices.Interfaces;
using Sellius.API.Domain.Extensions;
using Sellius.API.DTOs;

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
    public async Task<IActionResult> CadastrarMenu(MenuRegister dto)
    {
        var result = await commandService.CreateMenu(dto, User.GetEnterpriseId());
        if (result)
            return Ok(Response<bool>.Ok());
        return BadRequest(Response<bool>.Failed("Falha ao cadastrar menu"));
    }

    [HttpPut]
    [Authorize(Policy = "podeGerenciarUsuarios")]
    public async Task<IActionResult> AtualizarMenu(MenuRegister dto)
    {
        var result = await commandService.UpdateMenu(dto);
        if (result)
            return Ok(Response<bool>.Ok());
        return BadRequest(Response<bool>.Failed("Falha ao atualizar menu"));
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "podeGerenciarUsuarios")]
    public async Task<IActionResult> InativarMenu(long id)
    {
        var result = await commandService.InactiveMenu(id);
        if (result)
            return Ok(Response<bool>.Ok());
        return BadRequest(Response<bool>.Failed("Falha ao inativar menu"));
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> ObterMenu(long id)
    {
        var result = await queryService.FindByMenuId(id);
        return Ok(Response<MenuEdit>.Ok(result));
    }

    [HttpPost("list")]
    public async Task<IActionResult> ObterTodosMenus(MenuFilter filter)
    {
        var result = await queryService.FindAllMenus(filter, User.GetEnterpriseId());
        return Ok(Response<List<MenuRegister>>.Ok(result));
    }
}
