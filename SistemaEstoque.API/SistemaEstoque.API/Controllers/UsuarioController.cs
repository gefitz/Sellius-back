using Microsoft.AspNetCore.Mvc;
using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Models;
using Sellius.API.Services;

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
        public async Task<IActionResult> CadastroUsuario(UsuarioDTO usuarioDTO)
        {
            if (!ModelState.IsValid)
            {
                var menssagemErro = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return BadRequest(Response<UsuarioDTO>.Failed(menssagemErro));
            }
            var response = await _service.CriarUsuario(usuarioDTO);
            if(response.success)
            {
                return Ok(response); 
            }
            return BadRequest(response);

        }
        [HttpPost("obterTodosUsuarios")]
        public async Task<IActionResult> ObterTodosUsuarios(PaginacaoTabelaResult<UsuarioDTO, UsuarioDTO> paginacao)
        {
            var ret = await _service.ObterTodosUsuarios(paginacao);
            if (ret.success)
            {
                return Ok(ret);
            }
            return BadRequest(ret);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUsuario(UsuarioDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var menssagemErro = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return BadRequest(Response<UsuarioDTO>.Failed(menssagemErro));
            }
            var response = await _service.UpdateUsuario(dto);
            if (response.success) {
                return Ok(response);
             }
            return BadRequest(response);
        }
        [HttpDelete]
        public async Task<IActionResult> Inativar(int dto)
        {
            if(dto == 0)
            {
                return BadRequest(Response<UsuarioDTO>.Failed("O id deve ser maior do que zero"));
            }
            var response = await _service.InativarUsuario(dto);
            if (response.success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
