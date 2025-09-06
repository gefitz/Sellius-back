using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.DTOs.CadastrosDTOs.ClientesCadastros;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Repository.Interfaces;
using Sellius.API.Services.segmentacaos;
using Sellius.API.Utils;

namespace Sellius.API.Controllers.Clientes
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class SegmentacaoController: Controller
    {
        private readonly SegmentacaoService _service;

        public SegmentacaoController(SegmentacaoService service)
        {
            _service = service;
        }
        [HttpPost("novo")]
        public async Task<IActionResult> NovoSegmentacao(SegmentacaoDTO segmento)
        {
            segmento.idEmpresa = TokenService.RecuperaIdEmpresa(User);
            var response = await _service.CriarSegmento(segmento);
            if (response.success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPost("listaSegmentacao")]
        public async Task<IActionResult> Paginacao(PaginacaoTabelaResult<SegmentacaoDTO, SegmentacaoDTO> segmento)
        {
            segmento.Filtro.idEmpresa = TokenService.RecuperaIdEmpresa(User);
            var response = await _service.Buscarsegmentacaos(segmento);
            if (response.success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(SegmentacaoDTO segmento)
        {
            segmento.idEmpresa = TokenService.RecuperaIdEmpresa(User);
            var response = await _service.Updatesegmentacao(segmento);
            if (response.success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.Inativarsegmentacao(id);
            if (response.success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpGet("carregarComboSegmentacao")]
        public async Task<IActionResult> carregarCombo()
        {
            int idEmpresa = TokenService.RecuperaIdEmpresa(User);

            var response = await _service.CarregarCombo(idEmpresa);

            if (response.success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
