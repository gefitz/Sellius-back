using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Services.SaleOrderServices.CommandServices.Interfaces;
using Sellius.API.Application.Services.SaleOrderServices.QueryServices.Interfaces;
using Sellius.API.Domain.Extensions;
using Sellius.API.DTOs;

namespace Sellius.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class PedidoController(
    ISaleOrderCommandService commandService,
    ISaleOrderQueryService queryService) : Controller
{
    [HttpPost]
    [Authorize(Policy = "podeCriar")]
    public async Task<IActionResult> CadastrarPedido(SaleOrderRegister dto)
    {
        var result = await commandService.CreateSaleOrder(dto, User.GetUserId(), User.GetEnterpriseId());
        if (result)
            return Ok(Response<bool>.Ok());
        return BadRequest(Response<bool>.Failed("Falha ao cadastrar pedido"));
    }

    [HttpPut]
    [Authorize(Policy = "podeEditar")]
    public async Task<IActionResult> AtualizarPedido(SaleOrderRegister dto)
    {
        var result = await commandService.UpdateSaleOrder(dto);
        if (result)
            return Ok(Response<bool>.Ok());
        return BadRequest(Response<bool>.Failed("Falha ao atualizar pedido"));
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "podeInativar")]
    public async Task<IActionResult> CancelarPedido(long id)
    {
        var result = await commandService.CancelSaleOrder(id);
        if (result)
            return Ok(Response<bool>.Ok());
        return BadRequest(Response<bool>.Failed("Falha ao cancelar pedido"));
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> ObterPedido(long id)
    {
        var result = await queryService.FindBySaleOrderId(id);
        return Ok(Response<SaleOrderEdit>.Ok(result));
    }

    [HttpPost("list")]
    public async Task<IActionResult> ObterTodosPedidos(SaleOrderFilter filter)
    {
        var result = await queryService.FindAllSaleOrders(filter, User.GetEnterpriseId());
        return Ok(Response<List<SaleOrderTableReturn>>.Ok(result));
    }
}
