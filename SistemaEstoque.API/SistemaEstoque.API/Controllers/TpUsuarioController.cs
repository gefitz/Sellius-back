using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Services.TypeUserServices.CommandServices.Interfaces;
using Sellius.API.Application.Services.TypeUserServices.QueryServices.Interfaces;
using Sellius.API.Domain.Extensions;
using Sellius.API.DTOs;
using Sellius.API.DTOs.Filtros;

namespace Sellius.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class TpUsuarioController(
    ITypeUserCommandService commandService,
    ITypeUserQueryService queryService) : Controller
{
    [HttpPost]
    [Authorize(Policy = "podeGerenciarUsuarios")]
    public async Task<IActionResult> CadastrarTpUsuario(TypeUserRegister dto)
    {
        dto.EnterpriseId = User.GetEnterpriseId();
        var result = await commandService.CreateTypeUser(dto);
        if (result)
            return Ok(Response<bool>.Ok());
        return BadRequest(Response<bool>.Failed("Falha ao cadastrar tipo de usuário"));
    }

    [HttpPut]
    [Authorize(Policy = "podeGerenciarUsuarios")]
    public async Task<IActionResult> AtualizarTpUsuario(TypeUserRegister dto)
    {
        var result = await commandService.UpdateTypeUser(dto);
        if (result)
            return Ok(Response<bool>.Ok());
        return BadRequest(Response<bool>.Failed("Falha ao atualizar tipo de usuário"));
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "podeGerenciarUsuarios")]
    public async Task<IActionResult> InativarTpUsuario(long id)
    {
        var result = await commandService.InactiveTypeUser(id);
        if (result)
            return Ok(Response<bool>.Ok());
        return BadRequest(Response<bool>.Failed("Falha ao inativar tipo de usuário"));
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> ObterTpUsuario(long id)
    {
        var result = await queryService.FindByTypeUserId(id);
        return Ok(Response<TypeUserEdit>.Ok(result));
    }

    [HttpPost("list")]
    public async Task<IActionResult> ObterTodosTpUsuarios(TypeUserFilter filter)
    {
        var result = await queryService.FindAllTypeUsers(filter, User.GetEnterpriseId());
        return Ok(Response<List<TypeUserRegister>>.Ok(result));
    }
}
