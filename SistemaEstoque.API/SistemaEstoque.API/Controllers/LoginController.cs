using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.Models;
using Sellius.API.Services;
using System;

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

            Response.Cookies.Append("auth_token", response.Data, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None,// Lax funciona perfeitamente em localhost
                Expires = DateTimeOffset.UtcNow.AddHours(8),
                Path = "/"
                // Sem Domain, sem nada mais
            });

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
            if (ret.success)
                return Ok(ret);
            return BadRequest(ret);
        }

        [HttpDelete("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("auth_token", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });

            return Ok(Response<string>.Ok());
        }
        [HttpGet("permissoes")]
        [Authorize]
        public IActionResult permissoes()
        {
            var user = HttpContext.User;
            //var config = user.Identity.c
            TpUsuarioConfiguracaoDTO config = new TpUsuarioConfiguracaoDTO
            {
                flPodeAprovar = user.FindFirst("podeAprovar")?.Value == "True",
                flPodeCriar = user.FindFirst("podeCriar")?.Value == "True",
                flPodeEditar = user.FindFirst("podeEditar")?.Value == "True",
                flPodeExcluir = user.FindFirst("podeExcluir")?.Value == "True",
                flPodeExportar = user.FindFirst("podeExportar")?.Value == "True",
                flPodeGerenciarUsuarios = user.FindFirst("podeGerenciarUsuarios")?.Value == "True",
                flPodeInativar = user.FindFirst("podeInativar")?.Value == "True"
            };
            return Ok(Response<TpUsuarioConfiguracaoDTO>.Ok(config));
        }
    }
}
