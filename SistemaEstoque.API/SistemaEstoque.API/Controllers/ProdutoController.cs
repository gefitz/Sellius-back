using Sellius.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.DTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.Utils;
using Sellius.API.Services.Produtos;

namespace Sellius.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
    public class ProdutoController : Controller
    {
        private readonly ProdutoService _service;

        public ProdutoController(ProdutoService service)
        {
            _service = service;
        }
        [HttpPost("ObterProduto")]
        public async Task<IActionResult> ObterProduto(PaginacaoTabelaResult<ProdutoDTO,FiltroProduto> produto)
        {
            var response = await _service.FiltrarProduto(produto);
            if (response.success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPost("CadastrarProduto")]
        public async Task<IActionResult> CadastrarProduto(ProdutoDTO produtoDTO)
        {
            if (!ModelState.IsValid)
            {
                var menssagemErro = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return BadRequest(Response<ProdutoDTO>.Failed(menssagemErro));
            }
            produtoDTO.EmpresaId = TokenService.RecuperaIdEmpresa(User);
           var response = await _service.CadastrarProduto(produtoDTO);
            if (!response.success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduto(ProdutoDTO produtoDTO)
        {
            if (!ModelState.IsValid)
            {
                var menssagemErro = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return BadRequest(Response<ProdutoDTO>.Failed(menssagemErro));
            }
            produtoDTO.EmpresaId = TokenService.RecuperaIdEmpresa(User);
            var response = await _service.Update(produtoDTO);
            if (response.success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete]
        public async Task<IActionResult> InativarProduto(int id)
        {
            var ret = await _service.InativarProduto(id);
            if (ret.success)
            {
                return Ok(ret);
            }
            return BadRequest(ret);
        }

    }
}
