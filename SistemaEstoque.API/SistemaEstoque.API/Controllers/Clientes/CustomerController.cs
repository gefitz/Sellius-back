using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.RegisterDTOs.CustomerRegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Services.CustomerServices.CommandServices.Interfaces;
using Sellius.API.Application.Services.CustomerServices.QueryServices.Interfaces;
using Sellius.API.Domain.Extensions;

namespace Sellius.API.Controllers.Clientes;

[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class CustomerController(
    ICustomerCommandService commandService,
    ICustomerQueryService queryService) : Controller
{
    [HttpPost]
    [Authorize(Policy = "podeCriar")]
    public async Task<IActionResult> Create(CustomerRegister dto)
    {
        var result = await commandService.CreateCustomer(dto, User.GetEnterpriseId());
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to create customer" });
    }

    [HttpPut]
    [Authorize(Policy = "podeEditar")]
    public async Task<IActionResult> Update(CustomerRegister dto)
    {
        var result = await commandService.UpdateCustomer(dto);
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to update customer" });
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "podeInativar")]
    public async Task<IActionResult> Inactivate(long id)
    {
        var result = await commandService.InactiveCustomer(id);
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to inactivate customer" });
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await queryService.FindByCustomerId(id);
        return Ok(result);
    }

    [HttpPost("list")]
    public async Task<IActionResult> GetAll(CustomerFilter filter)
    {
        var result = await queryService.FindAllCustomers(filter, User.GetEnterpriseId());
        return Ok(result);
    }
}
