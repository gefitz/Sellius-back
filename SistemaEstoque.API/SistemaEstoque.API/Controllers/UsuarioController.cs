using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Services.UserServices.CommandServices.Interfaces;
using Sellius.API.Application.Services.UserServices.QueryServices.Interfaces;
using Sellius.API.Domain.Extensions;
using Sellius.API.DTOs;

namespace Sellius.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class UsuarioController(
    IUserCommandService commandService,
    IUserQueryService queryService) : Controller
{
    [HttpPost]
    [Authorize(Policy = "podeCriar")]
    public async Task<IActionResult> CadastrarUsuario(UserRegister dto)
    {
        var result = await commandService.CreateUser(dto,User.GetEnterpriseId());
        if (result)
            return Ok(Response<bool>.Ok());
        return BadRequest(Response<bool>.Failed("Falha ao cadastrar usuário"));
    }

    [HttpPut]
    [Authorize(Policy = "podeEditar")]
    public async Task<IActionResult> AtualizarUsuario(UserRegister dto)
    {
        var result = await commandService.UpdateUser(dto);
        if (result)
            return Ok(Response<bool>.Ok());
        return BadRequest(Response<bool>.Failed("Falha ao atualizar usuário"));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "podeInativar")]
    public async Task<IActionResult> InativarUsuario(Guid id)
    {
        var result = await commandService.InactiveUser(id);
        if (result)
            return Ok(Response<bool>.Ok());
        return BadRequest(Response<bool>.Failed("Falha ao inativar usuário"));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterUsuario(Guid id)
    {
        var result = await queryService.FindByUserId(id);
        return Ok(Response<UserEdit>.Ok(result));
    }

    [HttpPost("list")]
    public async Task<IActionResult> ObterTodosUsuarios(UserFilter filter)
    {
        var result = await queryService.FindAllUser(filter, User.GetEnterpriseId());
        return Ok(Response<object>.Ok(result));
    }
}
