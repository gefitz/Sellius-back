using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Services.UserServices.CommandServices.Interfaces;
using Sellius.API.Application.Services.UserServices.QueryServices.Interfaces;
using Sellius.API.Domain.Extensions;

namespace Sellius.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class UserController(
    IUserCommandService commandService,
    IUserQueryService queryService) : Controller
{
    [HttpPost]
    [Authorize(Policy = "podeCriar")]
    public async Task<IActionResult> Create(UserRegister dto)
    {
        var result = await commandService.CreateUser(dto, User.GetEnterpriseId());
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to create user" });
    }

    [HttpPut]
    [Authorize(Policy = "podeEditar")]
    public async Task<IActionResult> Update(UserRegister dto)
    {
        var result = await commandService.UpdateUser(dto);
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to update user" });
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "podeInativar")]
    public async Task<IActionResult> Inactivate(Guid id)
    {
        var result = await commandService.InactiveUser(id);
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to inactivate user" });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await queryService.FindByUserId(id);
        return Ok(result);
    }

    [HttpPost("list")]
    public async Task<IActionResult> GetAll(UserFilter filter)
    {
        var result = await queryService.FindAllUser(filter, User.GetEnterpriseId());
        return Ok(result);
    }
}
