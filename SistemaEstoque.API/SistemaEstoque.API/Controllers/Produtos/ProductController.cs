using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Services.ProductServices.CommandServices.Interfaces;
using Sellius.API.Application.Services.ProductServices.QueryServices.Interfaces;
using Sellius.API.Domain.Extensions;
using Sellius.API.DTOs.Filtros;

namespace Sellius.API.Controllers.Produtos;

[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class ProductController(
    IProductCommandServices commandService,
    IProductQueryService queryService) : Controller
{
    [HttpPost]
    [Authorize(Policy = "podeCriar")]
    public async Task<IActionResult> Create(ProductRegister dto)
    {
        var result = await commandService.CreateProduct(dto, User.GetEnterpriseId());
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to create product" });
    }

    [HttpPut]
    [Authorize(Policy = "podeEditar")]
    public async Task<IActionResult> Update(ProductRegister dto)
    {
        var result = await commandService.UpdateProduct(dto);
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to update product" });
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "podeInativar")]
    public async Task<IActionResult> Inactivate(long id)
    {
        var result = await commandService.InactiveProduct(id);
        if (result)
            return Ok();
        return BadRequest(new { error = "Failed to inactivate product" });
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await queryService.FindByProductId(id);
        return Ok(result);
    }

    [HttpPost("list")]
    public async Task<IActionResult> GetAll(ProductFilter filter)
    {
        var result = await queryService.FindAllProducts(filter, User.GetEnterpriseId());
        return Ok(result);
    }
}
