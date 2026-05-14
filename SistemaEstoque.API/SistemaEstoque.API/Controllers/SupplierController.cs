using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Services.SupplierServices.CommandServices.Interfaces;
using Sellius.API.Application.Services.SupplierServices.QueryServices.Interfaces;
using Sellius.API.Domain.Extensions;
using Sellius.API.DTOs.Filtros;

namespace Sellius.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class SupplierController(
    ISupplierCommandService commandService,
    ISupplierQueryService queryService) : Controller
{
    [HttpPost]
    [Authorize(Policy = "podeCriar")]
    public async Task<IActionResult> Create(SupplierRegister dto)
    {
        var result = await commandService.CreateSupplier(dto, User.GetEnterpriseId());
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to create supplier" });
    }

    [HttpPut]
    [Authorize(Policy = "podeEditar")]
    public async Task<IActionResult> Update(SupplierRegister dto)
    {
        var result = await commandService.UpdateSupplier(dto);
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to update supplier" });
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "podeInativar")]
    public async Task<IActionResult> Inactivate(long id)
    {
        var result = await commandService.InactiveSupplier(id);
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to inactivate supplier" });
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await queryService.FindBySupplierId(id);
        return Ok(result);
    }

    [HttpPost("list")]
    public async Task<IActionResult> GetAll(SupplierFilter filter)
    {
        var result = await queryService.FindAllSuppliers(filter, User.GetEnterpriseId());
        return Ok(result);
    }
}
