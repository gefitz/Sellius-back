using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Services.GroupCustomerServices.CommandServices.Interfaces;
using Sellius.API.Application.Services.GroupCustomerServices.QueryServices.Interfaces;
using Sellius.API.Domain.Extensions;

namespace Sellius.API.Controllers.Clientes;

[Authorize]
[ApiController]
[Route("/api/[controller]")]
public class GroupCustomerController(
    IGroupCustomerCommandService commandService,
    IGroupCustomerQueryService queryService) : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = "podeCriar")]
    public async Task<IActionResult> Create(GroupCustomerRegister dto)
    {
        var result = await commandService.CreateGroupCustomer(dto, User.GetEnterpriseId());
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to create customer group" });
    }

    [HttpPut]
    [Authorize(Policy = "podeEditar")]
    public async Task<IActionResult> Update(GroupCustomerRegister dto)
    {
        var result = await commandService.UpdateGroupCustomer(dto);
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to update customer group" });
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "podeInativar")]
    public async Task<IActionResult> Inactivate(long id)
    {
        var result = await commandService.InactiveGroupCustomer(id);
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to inactivate customer group" });
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await queryService.FindByGroupCustomerId(id);
        return Ok(result);
    }

    [HttpPost("list")]
    public async Task<IActionResult> GetAll(GroupCustomerFilter filter)
    {
        var result = await queryService.FindAllGroupCustomers(filter, User.GetEnterpriseId());
        return Ok(result);
    }
}
