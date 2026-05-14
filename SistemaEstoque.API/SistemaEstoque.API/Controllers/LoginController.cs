using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Services.AuthenticationServices.CommandServices.Interfaces;
using Sellius.API.Application.Services.AuthenticationServices.QueryServices.Interfaces;
using Sellius.API.Domain.Entity.EntityUsers;
using DomainUserConfiguration = Sellius.API.Domain.Entity.EntityUsers.UserConfiguration;

namespace Sellius.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class LoginController(
    IAuthenticationCommandServices commandServices,
    IAuthenticationQueryService queryService)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login(LoginRegister dto)
    {
        var token = await queryService.Login(dto);

        if (token is null)
            return BadRequest(new { error = "Invalid credentials." });

        Response.Cookies.Append("auth_token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            IsEssential = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddHours(8),
            Path = "/"
        });

        return Ok();
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(LoginRegister dto)
    {
        if (!ModelState.IsValid)
        {
            var errors = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
            return BadRequest(new { error = errors });
        }

        var result = await commandServices.UpdatePassword(dto);
        if (!result)
            return BadRequest(new { error = "Failed to change password." });

        return Ok();
    }

    [HttpPost("create-client-login")]
    [Authorize(Roles = "Adm,Gerente")]
    public async Task<IActionResult> CreateClientLogin(LoginRegister dto)
    {
        if (!ModelState.IsValid)
        {
            var errors = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
            return BadRequest(new { error = errors });
        }

        var result = await commandServices.CreateLogin(dto);
        if (!result)
            return BadRequest(new { error = "Failed to create login." });

        return Ok();
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

        return Ok();
    }

    [HttpGet("permissions")]
    [Authorize]
    public IActionResult GetPermissions()
    {
        var user = HttpContext.User;
        var config = new DomainUserConfiguration
        {
            PermissionApprove = user.FindFirst("podeAprovar")?.Value == "True",
            PermissionCreate = user.FindFirst("podeCriar")?.Value == "True",
            PermissionEdit = user.FindFirst("podeEditar")?.Value == "True",
            PermissionDelete = user.FindFirst("podeExcluir")?.Value == "True",
            PermissionExport = user.FindFirst("podeExportar")?.Value == "True",
            PermissionControlUser = user.FindFirst("podeGerenciarUsuarios")?.Value == "True",
            PermissionInactivate = user.FindFirst("podeInativar")?.Value == "True"
        };
        return Ok(config);
    }
}
