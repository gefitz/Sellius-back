using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Repository.Interfaces;
using Sellius.API.Services;
using Sellius.API.Utils;

namespace Sellius.API.Controllers
{
    [ApiController]
    [Route("/api/Usuario/[controller]")]
    [Authorize]
    public class TpUsuarioController : Controller
    {
        private TpUsuarioService _service;

        public TpUsuarioController(TpUsuarioService service)
        {
            _service = service;
        }
        [HttpPost("cadastrarTpUsuario")]
        public async Task<IActionResult> cadastrarTpUsuario(TpUsuarioDTO tpUsuario)
        {

            tpUsuario.idEmpresa = TokenService.RecuperaIdEmpresa(User);
            var ret = await _service.cadastrarTpUsuario(tpUsuario);
            if (ret != null && ret.success)
            {
                return Ok(ret);
            }
            return BadRequest(ret);
        }
        [HttpPost("paginacao")]
        public async Task<IActionResult> paginacao(PaginacaoTabelaResult<TpUsuarioDTO, TpUsuarioFiltro> tpUsuario)
        {

            tpUsuario.Filtro.idEmpresa = TokenService.RecuperaIdEmpresa(User);
            var ret = await _service.paginacao(tpUsuario);
            if (ret != null && ret.success) { return Ok(ret); }
            return BadRequest(ret);
        }

        [HttpGet("recuperarTpUsuarios")]
        public async Task<IActionResult> recuperarTpUsuarios()
        {

            int idEmpresa = TokenService.RecuperaIdEmpresa(User);
            var ret = await _service.recuperaTpUsuarios(idEmpresa);
            if (ret != null && ret.success) { return Ok(ret); }
            return BadRequest(ret);
        }

        [HttpPut]
        public async Task<IActionResult> update(TpUsuarioDTO tpUsuario)
        {

            tpUsuario.idEmpresa = TokenService.RecuperaIdEmpresa(User);
            var ret = await _service.Update(tpUsuario);
            if (ret != null && ret.success) { return Ok(ret); }
            return BadRequest(ret);
        }
        [HttpDelete]
        public async Task<IActionResult> delete(int id)
        {
            var ret = await _service.deletarTpUsuario(id);
            if (ret != null && ret.success) { return Ok(ret); }
            return BadRequest(ret);
        }
    }
}
