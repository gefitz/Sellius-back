using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.Application.DTOs.RegisterDTOs.CustomerRegisterDTOs;
using Sellius.API.Domain.Models;
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
        public async Task<IActionResult> NovoGrupo(GroupCustomerRegister grupo)
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
        public async Task<IActionResult> NovoGrupo(PaginationTableResult<> grupo)
        {
            grupo.Filtro.idEmpresa = TokenService.RecuperaIdEmpresa(User);

            var ret = await _service.BuscarClientes(grupo);
            if (ret.success)
                return Ok(ret);
            return BadRequest(ret);
        }
        [HttpPut]
        public async Task<IActionResult> Update(GroupCustomerRegister grupo)
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
            int idEmpresa = TokenService.RecuperaIdEmpresa(User);
            var ret = await _service.CarregarCombo(idEmpresa);
            if (ret.success)
                return Ok(ret);
            return BadRequest(ret);
        }

    }
}
