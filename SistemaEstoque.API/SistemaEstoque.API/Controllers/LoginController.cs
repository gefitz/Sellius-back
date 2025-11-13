using Sellius.API.Models;
using Sellius.API.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs;

namespace Sellius.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class LoginController : Controller
    {
        private readonly LoginService _service;
        public LoginController(LoginService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO usuario)
        {
            if (!ModelState.IsValid)
            {
                var menssagemErro = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return BadRequest(Response<LoginDTO>.Failed(menssagemErro));
            }
            var response = await _service.LoginAutenticacao(usuario);
            if (!response.success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("AlterarSenha")]
        public async Task<IActionResult> AlterarSenha(LoginDTO login)
        {
            if (!ModelState.IsValid)
            {
                var menssagemErro = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return BadRequest(Response<LoginDTO>.Failed(menssagemErro));
            }
            var response = await _service.AlterarSenha(login);
            if (!response.success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("criarclientelogin")]
        [Authorize(Roles = "Adm,Gerente")]
        public async Task<IActionResult> CriarLoginCliente(LoginDTO login)
        {
            if (!ModelState.IsValid)
            {
                var menssagemErro = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return BadRequest(Response<LoginDTO>.Failed(menssagemErro));
            }
            var ret = await _service.CriarClienteLogin(login);
            if(ret.success)
                return Ok(ret);
            return BadRequest(ret);
        }
    }
}
