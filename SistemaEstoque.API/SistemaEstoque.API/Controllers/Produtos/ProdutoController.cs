using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Services.ProductServices.CommandServices.Interfaces;
using Sellius.API.Application.Services.ProductServices.QueryServices.Interfaces;
using Sellius.API.Domain.Extensions;
using Sellius.API.DTOs;
using Sellius.API.DTOs.Filtros;

namespace Sellius.API.Controllers.Produtos;

[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class ProdutoController(
    IProductCommandServices commandService,
    IProductQueryService queryService) : Controller
{
    [HttpPost]
    [Authorize(Policy = "podeCriar")]
    public async Task<IActionResult> CadastrarProduto(ProductRegister dto)
    {
        var result = await commandService.CreateProduct(dto, User.GetEnterpriseId());
        if (result)
            return Ok(Response<bool>.Ok());
        return BadRequest(Response<bool>.Failed("Falha ao cadastrar produto"));
    }

    [HttpPut]
    [Authorize(Policy = "podeEditar")]
    public async Task<IActionResult> AtualizarProduto(ProductRegister dto)
    {
        var result = await commandService.UpdateProduct(dto);
        if (result)
            return Ok(Response<bool>.Ok());
        return BadRequest(Response<bool>.Failed("Falha ao atualizar produto"));
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "podeInativar")]
    public async Task<IActionResult> InativarProduto(long id)
    {
        var result = await commandService.InactiveProduct(id);
        if (result)
            return Ok(Response<bool>.Ok());
        return BadRequest(Response<bool>.Failed("Falha ao inativar produto"));
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> ObterProduto(long id)
    {
        var result = await queryService.FindByProductId(id);
        return Ok(Response<ProductEdit>.Ok(result));
    }

    [HttpPost("list")]
    public async Task<IActionResult> ObterTodosProdutos(ProductFilter filter)
    {
        var result = await queryService.FindAllProducts(filter, User.GetEnterpriseId());
        return Ok(Response<List<ProductTableReturn>>.Ok(result));
    }
}
