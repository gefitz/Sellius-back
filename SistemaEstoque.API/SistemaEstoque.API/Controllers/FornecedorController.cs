using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.Models;
using Sellius.API.Services;
using Sellius.API.Utils;
using System.Text;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Domain.Models;

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
        public async Task<IActionResult> ObterTabelaFornecedor([FromBody] PaginationTableResult<> tipoProdutoDTO)
        {
            if(tipoProdutoDTO.Filtro == null)
            {
                tipoProdutoDTO.Filtro = new SupplierFilter();
            }
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
            SupplierRegister fornecedor = new SupplierRegister() { EmpresaId = TokenService.RecuperaIdEmpresa(User) };
            var response = await _service.CarregarComboFornecedor(fornecedor);
            return Ok(response);
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
        public async Task<IActionResult> CadastrarTpProduto([FromBody] SupplierRegister tipoProduto)
        {
            if (!ModelState.IsValid)
            {
                var menssagemErro = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return BadRequest(Response<SupplierRegister>.Failed(menssagemErro));
            }
            tipoProduto.EmpresaId = TokenService.RecuperaIdEmpresa(User);
            var ret = await _service.CadastrarFornecedor(tipoProduto);
            if (ret.success) { return Ok(ret); }
            return BadRequest(ret);


        }
        [HttpPut]
        public async Task<IActionResult> UpdateFornecedor(SupplierRegister dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(
                    Response<SupplierRegister>.Failed("Falta de campos obrigatorio no parametro")
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
            SupplierRegister dto = new SupplierRegister() { id = id, EmpresaId = TokenService.RecuperaIdEmpresa(User) };

            var result = await _service.InativarFornecedor(dto);
            if (result.success)
            {
                return Ok(result);
            }
            //ViewBag.Error = _log.Messagem;
            return BadRequest(result);

        }
        [HttpPost("addFornecedorXCliente")]
        public async Task<IActionResult> AddFornecedorXCliente(SupplierXCustomerTableReturn dto)
        {
            var result = await _service.AddFornecedorXCliente(dto);
            if (result.success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("obterFornecedorXClientePaginada")]
        public async Task<IActionResult> obterFornecedorXClientePaginada(PaginationTableResult<> dto)
        {
            var result = await _service.ObterFornecedorXClientePaginada(dto);
            if (result.success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("removerVinculoFornecedorXCliente")]
        public async Task<IActionResult> removerVinculoFornecedorXCliente(SupplierXCustomerTableReturn supplierXCliente)
        {
            var ret = await _service.removerVinculoFornecedorXCliente(supplierXCliente);
            if(ret.success) {
                return Ok(ret);
            }
            return BadRequest(ret);

        }
    }
}
