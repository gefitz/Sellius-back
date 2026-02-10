using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Services.Produtos;
using Sellius.API.Utils;

namespace Sellius.API.Controllers.Produtos
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
    public class TabelaPrecoController : Controller
    {
        private TabelaPrecoService _service;

        public TabelaPrecoController(TabelaPrecoService service)
        {
            _service = service;
        }

        [HttpPost("novaTabelaPreco")]
        [Authorize(Policy = "podeCriar")]
        public async Task<IActionResult> NovaTabelaPreco(TabelaPrecoDTO dto)
        {
            dto.idEmpresa = TokenService.RecuperaIdEmpresa(User);
            dto.idUsuario = TokenService.RecuperaIdUsuario(User);

            var ret = await _service.criarTabela(dto);

            if (ret.success)
            {
                return Ok(ret);
            }
            return BadRequest(ret);

        }
        [HttpPost("paginacaoTabelaPreco")]
        public async Task<IActionResult> PaginacaoTabelaPreco(PaginacaoTabelaResult<TabelaPrecoDTO, FiltroTabelaPreco> dto)
        {
            dto.Filtro.idEmpresa = TokenService.RecuperaIdEmpresa(User);

            var ret = await _service.ObterTodasTabelasPreco(dto);

            if (ret.success)
            {
                return Ok(ret);
            }
            return BadRequest(ret);

        }
        [HttpGet]
        public async Task<IActionResult> PaginacaoTabelaPreco(int idTabelaPreco)
        {

            TabelaPrecoDTO dto = new TabelaPrecoDTO()
            {
                Id = idTabelaPreco,
                idEmpresa = TokenService.RecuperaIdEmpresa(User)
            };

            var ret = await _service.BuscaDireto(dto);

            if (ret.success)
            {
                return Ok(ret);
            }
            return BadRequest(ret);

        }
        [HttpPut]
        [Authorize(Policy = "podeEditar")]
        public async Task<IActionResult> UpDate(TabelaPrecoDTO dto)
        {
            dto.idEmpresa = TokenService.RecuperaIdEmpresa(User);
            dto.idUsuario = TokenService.RecuperaIdUsuario(User);

            var ret = await _service.UpDateTabelaPreco(dto);

            if (ret.success)
            {
                return Ok(ret);
            }
            return BadRequest(ret);

        }
        [HttpDelete]
        [Authorize(Policy = "podeInativar")]
        public async Task<IActionResult> Inativar(int idTabelaPreco)
        {

            TabelaPrecoDTO dto = new TabelaPrecoDTO { Id = idTabelaPreco, idEmpresa = TokenService.RecuperaIdEmpresa(User) };
            var ret = await _service.InativarTabelaPreco(dto);

            if (ret.success)
            {
                return Ok(ret);
            }
            return BadRequest(ret);

        }
    }
}
