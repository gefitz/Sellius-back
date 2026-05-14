using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Services.SaleOrderServices.CommandServices.Interfaces;
using Sellius.API.Application.Services.SaleOrderServices.QueryServices.Interfaces;
using Sellius.API.Domain.Extensions;

namespace Sellius.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class SaleOrderController(
    ISaleOrderCommandService commandService,
    ISaleOrderQueryService queryService) : Controller
{
    [HttpPost]
    [Authorize(Policy = "podeCriar")]
    public async Task<IActionResult> Create(SaleOrderRegister dto)
    {
        var result = await commandService.CreateSaleOrder(dto, User.GetUserId(), User.GetEnterpriseId());
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to create sale order" });
    }

    [HttpPut]
    [Authorize(Policy = "podeEditar")]
    public async Task<IActionResult> Update(SaleOrderRegister dto)
    {
        var result = await commandService.UpdateSaleOrder(dto);
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to update sale order" });
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "podeInativar")]
    public async Task<IActionResult> Cancel(long id)
    {
        var result = await commandService.CancelSaleOrder(id);
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to cancel sale order" });
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await queryService.FindBySaleOrderId(id);
        return Ok(result);
    }

    [HttpPost("list")]
    public async Task<IActionResult> GetAll(SaleOrderFilter filter)
    {
        var result = await queryService.FindAllSaleOrders(filter, User.GetEnterpriseId());
        return Ok(result);
    }
}
