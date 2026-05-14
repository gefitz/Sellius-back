using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Services.TypeUserServices.CommandServices.Interfaces;
using Sellius.API.Application.Services.TypeUserServices.QueryServices.Interfaces;
using Sellius.API.Domain.Extensions;
using Sellius.API.DTOs.Filtros;

namespace Sellius.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class TypeUserController(
    ITypeUserCommandService commandService,
    ITypeUserQueryService queryService) : Controller
{
    [HttpPost]
    [Authorize(Policy = "podeGerenciarUsuarios")]
    public async Task<IActionResult> Create(TypeUserRegister dto)
    {
        dto.EnterpriseId = User.GetEnterpriseId();
        var result = await commandService.CreateTypeUser(dto);
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to create user type" });
    }

    [HttpPut]
    [Authorize(Policy = "podeGerenciarUsuarios")]
    public async Task<IActionResult> Update(TypeUserRegister dto)
    {
        var result = await commandService.UpdateTypeUser(dto);
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to update user type" });
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "podeGerenciarUsuarios")]
    public async Task<IActionResult> Inactivate(long id)
    {
        var result = await commandService.InactiveTypeUser(id);
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to inactivate user type" });
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await queryService.FindByTypeUserId(id);
        return Ok(result);
    }

    [HttpPost("list")]
    public async Task<IActionResult> GetAll(TypeUserFilter filter)
    {
        var result = await queryService.FindAllTypeUsers(filter, User.GetEnterpriseId());
        return Ok(result);
    }
}
