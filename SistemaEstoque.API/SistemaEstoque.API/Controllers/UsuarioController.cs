using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Models;
using Sellius.API.Services;
using Sellius.API.Utils;

namespace Sellius.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsuarioController:Controller
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpPost("novoUsuario")]
        [Authorize(Policy = "podeCriar")]
        public async Task<IActionResult> CadastroUsuario(UsuarioDTO usuarioDTO)
        {
            if (!ModelState.IsValid)
            {
                var menssagemErro = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return BadRequest(Response<UsuarioDTO>.Failed(menssagemErro));
            }
            usuarioDTO.EmpresaId = TokenService.RecuperaIdEmpresa(User);
            var response = await _service.CriarUsuario(usuarioDTO);
            if(response.success)
            {
                return Ok(response); 
            }
            return BadRequest(response);

        }
        [HttpPost("obterTodosUsuarios")]
        public async Task<IActionResult> ObterTodosUsuarios(PaginacaoTabelaResult<UsuarioTabela, UsuarioFiltro> paginacao)
        {
            paginacao.Filtro.idEmpresa = TokenService.RecuperaIdEmpresa(User);
            var ret = await _service.ObterTodosUsuarios(paginacao);
            if (ret.success)
            {
                return Ok(ret);
            }
            return BadRequest(ret);
        }
        [HttpPut]
        [Authorize(Policy = "podeEditar")]
        public async Task<IActionResult> UpdateUsuario(UsuarioDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var menssagemErro = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return BadRequest(Response<UsuarioDTO>.Failed(menssagemErro));
            }
            dto.EmpresaId = TokenService.RecuperaIdEmpresa(User);
            var response = await _service.UpdateUsuario(dto);
            if (response.success) {
                return Ok(response);
             }
            return BadRequest(response);
        }
        [HttpDelete]
        public async Task<IActionResult> Inativar(int id)
        {
            if(id == 0)
            {
                return BadRequest(Response<UsuarioDTO>.Failed("O id deve ser maior do que zero"));
            }
            UsuarioDTO dto  = new UsuarioDTO
            {
                id = id,
                EmpresaId = TokenService.RecuperaIdEmpresa(User)
            };
            var response = await _service.InativarUsuario(dto);
            if (response.success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpGet("obterUsuario")]
        public async Task<IActionResult> obterUsuario(int id)
        {
            UsuarioDTO dto = new UsuarioDTO
            {
                id = id,
                EmpresaId = TokenService.RecuperaIdEmpresa(User)
            };

            var resp = await _service.BuscaDiretoUsuario(dto);

            if (resp.success)
            {
                return Ok(resp);
            }
            return BadRequest(resp);
        }
    }
}
