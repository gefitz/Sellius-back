using Sellius.API.Models;
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
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.Utils;
using Sellius.API.Repository.Login.Interfaces;
using Sellius.API.Services.Clientes;

namespace Sellius.API.Services
{
    public class LoginService
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginRepository _repository;
        private readonly TokenService _tokenService;
        private readonly ClienteService _clienteService;
        public LoginService(IConfiguration configuration, ILoginRepository repository,TokenService tokenService, ClienteService cliente)
        {
            _configuration = configuration;
            _repository = repository;
            _tokenService = tokenService;
            _clienteService = cliente;
        }
        public async Task<Response<string>> CriarLogin(LoginDTO login, UsuarioDTO usuario)
        {
            LoginModel model = login;
            model.usuarioId = usuario.id;
            model.EmpresaId = (int)usuario.EmpresaId;
            model.TipoUsuario = usuario.TipoUsuario;
           CriptografiaSenha(login.Password,model);

            if (await _repository.Create(model))
            {
                var token = await _tokenService.GerarCookie(model);
                return token;
            }
            return Response<string>.Failed("Erro ao salvar o login");
        }
        public async Task<Response<string>> LoginAutenticacao(LoginDTO login)
        {
            try
            {
                LoginModel usuarioAutenticar = await _repository.BuscaDireto(login);
                if (usuarioAutenticar == null)
                    return Response<string>.Failed("Falha ao encontrar o email cadastrado");
                if (!await ValidaSenha(usuarioAutenticar, login.Password))
                {
                    return Response<string>.Failed("Senha incorreta");
                }
                return await _tokenService.GerarCookie(usuarioAutenticar);

            }
            catch (Exception ex)
            {
                return Response<string>.Failed(ex.Message);
            }
        }
        public async Task<Response<LoginDTO>> AlterarSenha(LoginDTO login)
        {
            try
            {
                LoginModel loginRaiz = await _repository.BuscaDireto(login);
                if (loginRaiz == null)
                    return Response<LoginDTO>.Failed("Usuario não encontrado");
                 CriptografiaSenha(login.Password,loginRaiz);
                if (!await _repository.Update(loginRaiz))
                    return Response<LoginDTO>.Failed("Falha ao tentar alterar a senha");
            }
            catch (Exception ex)
            {
                return Response<LoginDTO>.Failed(ex.Message);
            }
            return Response<LoginDTO>.Ok();
        }
        public async Task<Response<string>> CriarClienteLogin(LoginDTO login)
        {
            try
            {
                LoginModel loginModel = login;
                loginModel.usuarioId = null;
                loginModel.TipoUsuario = TipoUsuario.Cliente;
                loginModel = CriptografiaSenha(login.Password, loginModel);
                if(await _repository.VereficaEmailExistente(loginModel))
                {
                    return Response<string>.Failed("Esse email já está sendo utilizado na sua base");
                }
                var cliente = await _clienteService.BuscarId(Convert.ToInt32(loginModel.usuarioId));
                if (!cliente.success)
                {
                    return Response<string>.Failed("Cliente não localizado para está criando o login");
                }

                if (await _repository.Create(loginModel))
                {
                    return await _tokenService.GerarCookie(loginModel);
                }
                return Response<string>.Failed("Falha ao criar o login de acesso do cliente");
            }
            catch (Exception ex) {
                return Response<string>.Failed(ex.Message);
            }
        }

        #region Metodos Privados
        private async Task<bool> ValidaSenha(LoginModel usuario, string password)
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
        private LoginModel CriptografiaSenha(string password, LoginModel login)
        {
            try
            {
                using (var hmac = new HMACSHA512())
                {
                    login.Hash  = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                   login.Salt = hmac.Key;
                }
                return login;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Falha ao criptografar a senha: " + ex.Message);
            }
        }
        #endregion

    }
}
