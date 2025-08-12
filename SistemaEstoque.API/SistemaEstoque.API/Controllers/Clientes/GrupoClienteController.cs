using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.DTOs.CadastrosDTOs.ClientesCadastros;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Services.Clientes;
using Sellius.API.Utils;

namespace Sellius.API.Controllers.Clientes
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class GrupoClienteController : Controller
    {
        private readonly GrupoClienteService _service;

        public GrupoClienteController(GrupoClienteService service)
        {
            _service = service;
        }
        [HttpPost("novo")]
        public async Task<IActionResult> NovoGrupo(GrupoClienteDTO grupo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            grupo.idEmpresa = TokenService.RecuperaIdEmpresa(User);
            var ret = await _service.CadastrarGrupo(grupo);
            if(ret.success)
                return Ok(ret);
            return BadRequest(ret);        
        }
        [HttpPost("listaGrupo")]
        public async Task<IActionResult> NovoGrupo(PaginacaoTabelaResult< GrupoClienteDTO,GrupoClienteDTO> grupo)
        {
            grupo.Filtro.idEmpresa = TokenService.RecuperaIdEmpresa(User);

            var ret = await _service.BuscarClientes(grupo);
            if (ret.success)
                return Ok(ret);
            return BadRequest(ret);
        }
        [HttpPut]
        public async Task<IActionResult> Update(GrupoClienteDTO grupo)
        {
            grupo.idEmpresa = TokenService.RecuperaIdEmpresa(User);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var ret = await _service.UpdateGrupo(grupo);
            if (ret.success)
                return Ok(ret);
            return BadRequest(ret);
        }
        [HttpDelete]
        public async Task<IActionResult> Inativar(int id)
        {
            var ret = await _service.InativarCliente(id);
            if (ret.success)
                return Ok(ret);
            return BadRequest(ret);
        }
        [HttpGet("carregarComboGrupo")]
        public async Task<IActionResult> CarregarCombo()
        {
            GrupoClienteDTO grupo = new GrupoClienteDTO() { idEmpresa = TokenService.RecuperaIdEmpresa(User)};
            var ret = await _service.CadastrarGrupo(grupo);
            if (ret.success)
                return Ok(ret);
            return BadRequest(ret);
        }

    }
}
