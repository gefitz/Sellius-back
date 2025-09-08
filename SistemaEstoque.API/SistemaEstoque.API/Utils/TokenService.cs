using Microsoft.IdentityModel.Tokens;
using Sellius.API.DTOs;
using Sellius.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sellius.API.Utils
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<Response<string>> GerarCookie(LoginModel login)
        {
            #region Claims
            Claim[] claims = [];
            string idUsuarioClient = "";
            string user = "";

                idUsuarioClient = login.usuarioId.ToString();
                user = login.Usuario.Nome;

            claims = new[]
                  {
                        new Claim("id", idUsuarioClient),
                        new Claim("user", user),
                        new Claim(ClaimTypes.Role, Enum.GetName(login.TipoUsuario)),
                        new Claim("empresa", login.EmpresaId.ToString()),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

            #endregion

            #region GeraToken
            try
            {
                var privateKy = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:secretkey"]));

                var crendentials = new SigningCredentials(privateKy, SecurityAlgorithms.HmacSha256);

                var expiration = DateTime.UtcNow.AddDays(1);

                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: _configuration["jwt:issuer"],
                    audience: _configuration["jwt:audience"],
                    claims: claims,
                    expires: expiration,
                    signingCredentials: crendentials);
                return Response<string>.Ok(new JwtSecurityTokenHandler().WriteToken(token));

            }
            catch (Exception ex)
            {
                return Response<string>.Failed(ex.Message);
            }
            #endregion

        }


        public static int RecuperaIdEmpresa(ClaimsPrincipal identity)
        {
            return Int32.Parse(identity.FindFirst("empresa")?.Value);
        }
        public static int RecuperaIdUsuario(ClaimsPrincipal identity)
        {
            return Int32.Parse(identity.FindFirst("id")?.Value);
        }
    }
}
