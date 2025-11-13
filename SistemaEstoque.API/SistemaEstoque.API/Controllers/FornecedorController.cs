using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Services;
using Sellius.API.Utils;
using System.Text;

namespace Sellius.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
    public class FornecedorController : Controller
    {
        private readonly FornecedorService _service;

        public FornecedorController(FornecedorService service)
        {
            _service = service;
        }
        [HttpPost("ObterTabelaFornecedor")]
        public async Task<IActionResult> ObterTpProduto([FromBody]PaginacaoTabelaResult<FornecedorTabelaResult, FiltroFornecedor> tipoProdutoDTO)
        {
            tipoProdutoDTO.Filtro.EmpresaId = TokenService.RecuperaIdEmpresa(User);
            var ret = await _service.BuscarFornecedor(tipoProdutoDTO);
            if (!ret.success)
            {
                return BadRequest(ret);
            }
            return Ok(ret);
        }
        [HttpGet("carregarComboFornecedor")]
        public async Task<IActionResult> CarregarComboFornecedor()
        {
            FornecedorDTO fornecedor = new FornecedorDTO() { EmpresaId = TokenService.RecuperaIdEmpresa(User)};
            var response = await _service.CarregarComboFornecedor(fornecedor);
            return  Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> obterFornecedor(int idFornecedor)
        {
            var ret = await _service.BuscarId(idFornecedor);
            if (ret.success)
            {
                return Ok(ret);
            }
            return BadRequest(ret);
        }
        [HttpPost("NovoFornecedor")]
        public async Task<IActionResult> CadastrarTpProduto([FromBody] FornecedorDTO tipoProduto)
        {
            if (!ModelState.IsValid)
            {
                var menssagemErro = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return BadRequest(Response<FornecedorDTO>.Failed(menssagemErro));
            }
            tipoProduto.EmpresaId = TokenService.RecuperaIdEmpresa(User);
            var ret = await _service.CadastrarFornecedor(tipoProduto);
            if (ret.success) { return Ok(ret); }
            return BadRequest(ret);


        }
        [HttpPut]
        public async Task<IActionResult> UpdateFornecedor(FornecedorDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(
                    Response<FornecedorDTO>.Failed("Falta de campos obrigatorio no parametro")
                    );
            }
            dto.EmpresaId = TokenService.RecuperaIdEmpresa(User);
            var result = await _service.UpdateFornecedor(dto);
            if (result.success) { return Ok(result); }
            return BadRequest(result);
        }
        [HttpDelete]
        public async Task<IActionResult> InativarFornecedor(int id)
        {
            FornecedorDTO dto = new FornecedorDTO() { id = id, EmpresaId = TokenService.RecuperaIdEmpresa(User) };

            var result = await _service.InativarFornecedor(dto);
            if (result.success)
            {
                return Ok(result);
            }
            //ViewBag.Error = _log.Messagem;
            return BadRequest(result);

        }
    }
}
