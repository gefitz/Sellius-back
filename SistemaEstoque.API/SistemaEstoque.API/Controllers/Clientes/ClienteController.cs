using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.RegisterDTOs.CustomerRegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Services.CustomerServices.CommandServices.Interfaces;
using Sellius.API.Application.Services.CustomerServices.QueryServices.Interfaces;
using Sellius.API.Domain.Extensions;
using Sellius.API.DTOs;

namespace Sellius.API.Controllers.Clientes;

[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class ClienteController(
    ICustomerCommandService commandService,
    ICustomerQueryService queryService) : Controller
{
    [HttpPost]
    [Authorize(Policy = "podeCriar")]
    public async Task<IActionResult> CadastrarCliente(CustomerRegister dto)
    {
        var result = await commandService.CreateCustomer(dto, User.GetEnterpriseId());
        if (result)
            return Ok(Response<bool>.Ok());
        return BadRequest(Response<bool>.Failed("Falha ao cadastrar cliente"));
    }

    [HttpPut]
    [Authorize(Policy = "podeEditar")]
    public async Task<IActionResult> AtualizarCliente(CustomerRegister dto)
    {
        var result = await commandService.UpdateCustomer(dto);
        if (result)
            return Ok(Response<bool>.Ok());
        return BadRequest(Response<bool>.Failed("Falha ao atualizar cliente"));
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "podeInativar")]
    public async Task<IActionResult> InativarCliente(long id)
    {
        var result = await commandService.InactiveCustomer(id);
        if (result)
            return Ok(Response<bool>.Ok());
        return BadRequest(Response<bool>.Failed("Falha ao inativar cliente"));
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> ObterCliente(long id)
    {
        var result = await queryService.FindByCustomerId(id);
        return Ok(Response<CustomerEdit>.Ok(result));
    }

    [HttpPost("list")]
    public async Task<IActionResult> ObterTodosClientes(CustomerFilter filter)
    {
        var result = await queryService.FindAllCustomers(filter, User.GetEnterpriseId());
        return Ok(Response<List<CustomerTableReturn>>.Ok(result));
    }
}
