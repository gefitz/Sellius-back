using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Services.SegmentationServices.CommandServices.Interfaces;
using Sellius.API.Application.Services.SegmentationServices.QueryServices.Interfaces;
using Sellius.API.Domain.Extensions;

namespace Sellius.API.Controllers.Clientes;

[Authorize]
[ApiController]
[Route("/api/[controller]")]
public class SegmentationController(
    ISegmentationCommandService commandService,
    ISegmentationQueryService queryService) : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = "podeCriar")]
    public async Task<IActionResult> Create(SegmentationRegister dto)
    {
        var result = await commandService.CreateSegmentation(dto, User.GetEnterpriseId());
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to create segmentation" });
    }

    [HttpPut]
    [Authorize(Policy = "podeEditar")]
    public async Task<IActionResult> Update(SegmentationRegister dto)
    {
        var result = await commandService.UpdateSegmentation(dto);
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to update segmentation" });
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "podeInativar")]
    public async Task<IActionResult> Inactivate(long id)
    {
        var result = await commandService.InactiveSegmentation(id);
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to inactivate segmentation" });
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await queryService.FindBySegmentationId(id);
        return Ok(result);
    }

    [HttpPost("list")]
    public async Task<IActionResult> GetAll(SegmentationFilter filter)
    {
        var result = await queryService.FindAllSegmentations(filter, User.GetEnterpriseId());
        return Ok(result);
    }
}
