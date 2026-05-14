using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Services.PriceTableServices.CommandServices.Interfaces;
using Sellius.API.Application.Services.PriceTableServices.QueryServices.Interfaces;
using Sellius.API.Domain.Extensions;

namespace Sellius.API.Controllers.Produtos;

[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class PriceTableController(
    IPriceTableCommandService commandService,
    IPriceTableQueryService queryService) : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = "podeCriar")]
    public async Task<IActionResult> Create(PriceTableRegister dto)
    {
        var result = await commandService.CreatePriceTable(dto, User.GetEnterpriseId(), User.GetUserId());
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to create price table" });
    }

    [HttpPut]
    [Authorize(Policy = "podeEditar")]
    public async Task<IActionResult> Update(PriceTableRegister dto)
    {
        var result = await commandService.UpdatePriceTable(dto);
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to update price table" });
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await queryService.FindByPriceTableId(id);
        return Ok(result);
    }

    [HttpPost("list")]
    public async Task<IActionResult> GetAll(PriceTableFilter filter)
    {
        var result = await queryService.FindAllPriceTables(filter, User.GetEnterpriseId());
        return Ok(result);
    }
}
