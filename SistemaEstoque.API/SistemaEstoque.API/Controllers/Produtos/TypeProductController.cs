using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Services.TypeProductServices.CommandServices.Interfaces;
using Sellius.API.Application.Services.TypeProductServices.QueryServices.Interfaces;
using Sellius.API.Domain.Extensions;

namespace Sellius.API.Controllers.Produtos;

[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class TypeProductController(
    ITypeProductCommandService commandService,
    ITypeProductQueryService queryService) : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = "podeCriar")]
    public async Task<IActionResult> Create(TypeProductRegister dto)
    {
        var result = await commandService.CreateTypeProduct(dto, User.GetEnterpriseId());
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to create product type" });
    }

    [HttpPut]
    [Authorize(Policy = "podeEditar")]
    public async Task<IActionResult> Update(TypeProductRegister dto)
    {
        var result = await commandService.UpdateTypeProduct(dto);
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to update product type" });
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "podeInativar")]
    public async Task<IActionResult> Inactivate(long id)
    {
        var result = await commandService.InactiveTypeProduct(id);
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to inactivate product type" });
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await queryService.FindByTypeProductId(id);
        return Ok(result);
    }

    [HttpPost("list")]
    public async Task<IActionResult> GetAll(TypeProductFilter filter)
    {
        var result = await queryService.FindAllTypeProducts(filter, User.GetEnterpriseId());
        return Ok(result);
    }
}
