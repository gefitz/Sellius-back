using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Services.EnterpriseServices.CommandServices.Interfaces;
using Sellius.API.Application.Services.EnterpriseServices.QueryServices.Interfaces;
using Sellius.API.Domain.Extensions;
using Sellius.API.Domain.Traces;

namespace Sellius.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class EnterpriseController(
    IEnterpriseCommandService commandService,
    IEnterpriseQueryService queryService) : Controller
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Create(EnterpriseRegister dto)
    {
        var result = await commandService.CreateEnterprise(dto);
        if (result)
            return Ok();
        return BadRequest(new { error = EnterpriseTrace.CreateFailed });
    }

    [HttpPut]
    [Authorize(Roles = "Interno")]
    public async Task<IActionResult> Update(EnterpriseRegister dto)
    {
        var result = await commandService.UpdateEnterprise(dto);
        if (result)
            return Ok();
        return BadRequest(new { error = EnterpriseTrace.UpdateFailed });
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Interno")]
    public async Task<IActionResult> Inactivate(Guid id)
    {
        var result = await commandService.InactiveEnterprise(id);
        if (result)
            return Ok();
        return BadRequest(new { error = EnterpriseTrace.InactivateFailed });
    }

    [HttpGet]
    [Authorize(Roles = "Interno")]
    public async Task<IActionResult> GetById()
    {
        var result = await queryService.FindByEnterpriseId(User.GetEnterpriseId());
        return Ok(result);
    }

    [HttpGet("all")]
    [Authorize(Roles = "Interno")]
    public async Task<IActionResult> GetAll()
    {
        var result = await queryService.FindAllEnterprises();
        return Ok(result);
    }
}
