using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Services.SupplierServices.CommandServices.Interfaces;
using Sellius.API.Application.Services.SupplierServices.QueryServices.Interfaces;
using Sellius.API.Domain.Extensions;
using Sellius.API.DTOs;
using Sellius.API.DTOs.Filtros;

namespace Sellius.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class FornecedorController(
    ISupplierCommandService commandService,
    ISupplierQueryService queryService) : Controller
{
    [HttpPost]
    [Authorize(Policy = "podeCriar")]
    public async Task<IActionResult> CadastrarFornecedor(SupplierRegister dto)
    {
        var result = await commandService.CreateSupplier(dto, User.GetEnterpriseId());
        if (result)
            return Ok(Response<bool>.Ok());
        return BadRequest(Response<bool>.Failed("Falha ao cadastrar fornecedor"));
    }

    [HttpPut]
    [Authorize(Policy = "podeEditar")]
    public async Task<IActionResult> AtualizarFornecedor(SupplierRegister dto)
    {
        var result = await commandService.UpdateSupplier(dto);
        if (result)
            return Ok(Response<bool>.Ok());
        return BadRequest(Response<bool>.Failed("Falha ao atualizar fornecedor"));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "podeInativar")]
    public async Task<IActionResult> InativarFornecedor(int id)
    {
        var result = await commandService.InactiveSupplier(id);
        if (result)
            return Ok(Response<bool>.Ok());
        return BadRequest(Response<bool>.Failed("Falha ao inativar fornecedor"));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterFornecedor(int id)
    {
        var result = await queryService.FindBySupplierId(id);
        return Ok(Response<SupplierEdit>.Ok(result));
    }

    [HttpPost("list")]
    public async Task<IActionResult> ObterTodosFornecedores(SupplierFilter filter)
    {
        var result = await queryService.FindAllSuppliers(filter, User.GetEnterpriseId());
        return Ok(Response<List<SupplierTableReturn>>.Ok(result));
    }
}
