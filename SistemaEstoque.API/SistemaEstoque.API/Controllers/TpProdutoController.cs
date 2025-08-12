using Sellius.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs;
using Sellius.API.DTOs.TabelasDTOs;
using Microsoft.AspNetCore.Authorization;
using Sellius.API.Utils;
using Sellius.API.Services.Produtos;
namespace Sellius.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
    public class TpProdutoController : Controller
    {
        private readonly TpProdutoService _service;

        public TpProdutoController(TpProdutoService service)
        {
            _service = service;
        }
        [HttpGet("carregarCombo")]
        public async Task<IActionResult> CarregarCombo()
        {
            int idEmpresa = TokenService.RecuperaIdEmpresa(User);
            var ret = await _service.CarregarCombo(idEmpresa);
            if (ret.success) { return Ok(ret); }
            return BadRequest(ret);
        }
        [HttpPost("ObterTabelaTpProduto")]
        public async Task<IActionResult> ObterTpProduto(PaginacaoTabelaResult<TipoProdutoDTO, TipoProdutoDTO> tipoProdutoDTO)
        {
            tipoProdutoDTO.Filtro.EmpresaId = TokenService.RecuperaIdEmpresa(User);
            var ret = await _service.BuscarTpProudo(tipoProdutoDTO);
            if (!ret.success)
            {
                return BadRequest(ret);
            }
            return Ok(ret);
        }
        [HttpPost("NovoTpProduto")]
        public async Task<IActionResult> CadastrarTpProduto([FromBody] TipoProdutoDTO tipoProduto)
        {
            if (!ModelState.IsValid)
            {
                var menssagemErro = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return BadRequest(Response<TipoProdutoDTO>.Failed(menssagemErro));
            }
            tipoProduto.EmpresaId = TokenService.RecuperaIdEmpresa(User);
            var ret = await _service.CadastrarTpProduto(tipoProduto);
            if (ret.success) { return Ok(ret); }
            return BadRequest(ret);


        }
        [HttpPut]
        public async Task<IActionResult> UpdateTpProduto(TipoProdutoDTO dto)
        {
            dto.EmpresaId = TokenService.RecuperaIdEmpresa(User);
            if (!ModelState.IsValid)
            {
                var menssagemErro = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return BadRequest(Response<TipoProdutoDTO>.Failed(menssagemErro));
            }
            var result = await _service.UpdateTpProduto(dto);
            if (result.success) { return Ok(result); }
            return BadRequest(result);
        }
        [HttpDelete]
        public async Task<IActionResult> InativarTpProduto(int id)
        {

            var result = await _service.InativarTpProduto(id);
            if (result.success)
            {
                return Ok(result);
            }
            //ViewBag.Error = _log.Messagem;
            return BadRequest(result);

        }
    }
}
