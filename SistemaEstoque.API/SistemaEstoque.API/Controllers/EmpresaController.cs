using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Services.EnterpriseServices.CommandServices.Interfaces;
using Sellius.API.Application.Services.EnterpriseServices.QueryServices.Interfaces;
using Sellius.API.Domain.Extensions;
using Sellius.API.DTOs;

namespace Sellius.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class EmpresaController(
    IEnterpriseCommandService commandService,
    IEnterpriseQueryService queryService) : Controller
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CadastrarEmpresa(EnterpriseRegister dto)
    {
        var result = await commandService.CreateEnterprise(dto);
        if (result)
            return Ok(Response<bool>.Ok());
        return BadRequest(Response<bool>.Failed("Falha ao cadastrar empresa"));
    }

    [HttpPut]
    [Authorize(Roles = "Interno")]
    public async Task<IActionResult> AtualizarEmpresa(EnterpriseRegister dto)
    {
        var result = await commandService.UpdateEnterprise(dto);
        if (result)
            return Ok(Response<bool>.Ok());
        return BadRequest(Response<bool>.Failed("Falha ao atualizar empresa"));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Interno")]
    public async Task<IActionResult> InativarEmpresa(Guid id)
    {
        var result = await commandService.InactiveEnterprise(id);
        if (result)
            return Ok(Response<bool>.Ok());
        return BadRequest(Response<bool>.Failed("Falha ao inativar empresa"));
    }

    [HttpGet]
    [Authorize(Roles = "Interno")]
    public async Task<IActionResult> ObterEmpresa()
    {
        var result = await queryService.FindByEnterpriseId(User.GetEnterpriseId());
        return Ok(Response<EnterpriseEdit>.Ok(result));
    }

    [HttpGet("all")]
    [Authorize(Roles = "Interno")]
    public async Task<IActionResult> ObterTodasEmpresas()
    {
        var result = await queryService.FindAllEnterprises();
        return Ok(Response<List<EnterpriseRegister>>.Ok(result));
    }
}
