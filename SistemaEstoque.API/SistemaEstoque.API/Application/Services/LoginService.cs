using Sellius.API.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Sellius.API.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Sellius.API.Repository.Interfaces;
using Sellius.API.Enums;
using Microsoft.OpenApi.Extensions;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Domain.Entity.EntityUsers;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.Utils;
using Sellius.API.Repository.Login.Interfaces;
using Sellius.API.Services.Clientes;
using Sellius.API.Repository.Login;

namespace Sellius.API.Services
{
    public class LoginService
    {
        private readonly IConfiguration _configuration;
        private readonly LoginRepository _repository;
        private readonly TokenService _tokenService;
        private readonly ClienteService _clienteService;
        public LoginService(IConfiguration configuration, LoginRepository repository, TokenService tokenService, ClienteService cliente)
        {
            _configuration = configuration;
            _repository = repository;
            _tokenService = tokenService;
            _clienteService = cliente;
        }
        public async Task<Response<string>> CriarLogin(LoginRegister login, UserRegister usuario)
        {
            Authentication model = login;
            model.usuarioId = usuario.id;
            model.EmpresaId = (int)usuario.EmpresaId;
            //model.TipoUsuario = usuario.TipoUsuario;
            CriptografiaSenha(login.Password, model);

            if (await _repository.Create(model))
            {
                var token = await _tokenService.GerarCookie(model);
                return token;
            }
            return Response<string>.Failed("Erro ao salvar o login");
        }
        public async Task<Response<string>> LoginAutenticacao(LoginRegister login)
        {
            try
            {
                Authentication usuarioAutenticar = await _repository.BuscaDireto(login);
                if (usuarioAutenticar == null)
                    return Response<string>.Failed("Falha ao encontrar o email cadastrado");
                if (!await ValidaSenha(usuarioAutenticar, login.Password))
                {
                    return Response<string>.Failed("Senha incorreta");
                }
                if (usuarioAutenticar.Usuario.fAtivo == 0)
                {
                    return Response<string>.Failed("Usuario inativo, contate o admistrador do sistema para mais informacoes");
                }
                return await _tokenService.GerarCookie(usuarioAutenticar);

            }
            catch (Exception ex)
            {
                return Response<string>.Failed(ex.Message);
            }
        }
        public async Task<Response<LoginRegister>> AlterarSenha(LoginRegister login)
        {
            try
            {
                Authentication authenticationRaiz = await _repository.BuscaDireto(login);
                if (authenticationRaiz == null)
                    return Response<LoginRegister>.Failed("Usuario não encontrado");
                CriptografiaSenha(login.Password, authenticationRaiz);
                if (!await _repository.Update(authenticationRaiz))
                    return Response<LoginRegister>.Failed("Falha ao tentar alterar a senha");
            }
            catch (Exception ex)
            {
                return Response<LoginRegister>.Failed(ex.Message);
            }
            return Response<LoginRegister>.Ok();
        }
        public async Task<Response<string>> CriarClienteLogin(LoginRegister login)
        {
            try
            {
                Authentication authentication = authentication;
                authentication.usuarioId = null;
                //loginModel.TipoUsuario = TipoUsuario.Cliente;
                authentication = CriptografiaSenha(authentication.Password, authentication);
                if (await _repository.VereficaEmailExistente(authentication))
                {
                    return Response<string>.Failed("Esse email já está sendo utilizado na sua base");
                }
                var cliente = await _clienteService.BuscarId(Convert.ToInt32(authentication.usuarioId));
                if (!cliente.success)
                {
                    return Response<string>.Failed("Cliente não localizado para está criando o login");
                }

                if (await _repository.Create(authentication))
                {
                    return await _tokenService.GerarCookie(authentication);
                }
                return Response<string>.Failed("Falha ao criar o login de acesso do cliente");
            }
            catch (Exception ex)
            {
                return Response<string>.Failed(ex.Message);
            }
        }

        #region Metodos Privados
        private async Task<bool> ValidaSenha(Authentication usuario, string password)
        {
            try
            {
                using (var hmac = new HMACSHA512(usuario.Salt))
                {
                    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                    for (var i = 0; i < computedHash.Length; i++)
                    {
                        if (computedHash[i] != usuario.Hash[i])
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private Authentication CriptografiaSenha(string password, Authentication authentication)
        {
            try
            {
                using (var hmac = new HMACSHA512())
                {
                    authentication.Hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                    authentication.Salt = hmac.Key;
                }
                return authentication;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Falha ao criptografar a senha: " + ex.Message);
            }
        }
        #endregion

    }
}
