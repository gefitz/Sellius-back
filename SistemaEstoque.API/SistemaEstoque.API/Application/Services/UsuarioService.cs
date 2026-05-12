using AutoMapper;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Domain.Entity.EntityUsers;
using Sellius.API.Domain.Models;
using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.Infra.Repository.Users;
using Sellius.API.Repository.Interfaces;

namespace Sellius.API.Services
{
    public class UsuarioService
    {
        private readonly UserRepository _repository;
        private readonly LoginService _login;
        public UsuarioService(UserRepository repository, LoginService login)
        {
            _repository = repository;
            _login = login;
        }
        public async Task<Response<UserRegister>> CriarUsuario(UserRegister dTO)
        {
            try
            {

                User user = dTO;
                if (await VereficaExistenciaUsuario(dTO)) { return Response<UserRegister>.Failed("Email já esta sendo utilizado"); }
                if (await _repository.Create(user))
                {
                    dTO = user;
                    LoginRegister login = new LoginRegister
                    {
                        Email = user.Email
                    };


                    return Response<UserRegister>.Ok(dTO);
                }
                return Response<UserRegister>.Failed("Falha ao cadastrar usuario");
            }
            catch (Exception ex)
            {
                return Response<UserRegister>.Failed(ex.Message);

            }

        }
        public async Task<bool> VereficaExistenciaUsuario(UserRegister dto)
        {
            User usiario = await _repository.BuscaDiretoEmail(dto);
            if (usiario != null)
                return true;
            return false;
        }

        public async Task<Response<UserRegister>> UpdateUsuario(UserRegister usuario)
        {
            try
            {
                User model = usuario;
                if (await _repository.Update(model))
                {
                    var retLogin = await _login.AlterarSenha(usuario.login);
                    if (retLogin.success)
                    {
                        return Response<UserRegister>.Ok(model);
                    }
                    else
                    {
                        return Response<UserRegister>.Failed(retLogin.errorMessage);
                    }
                }
                return Response<UserRegister>.Failed("Falha ao tentar fazer upadte no usuario");
            }
            catch (Exception ex)
            {
                return Response<UserRegister>.Failed(ex.Message);
            }
        }

        public async Task<Response<UserRegister>> BuscaDiretoUsuario(UserRegister usuario)
        {
            try
            {
                usuario = await _repository.BuscaDireto(usuario);
                if (usuario != null)
                    return Response<UserRegister>.Ok(usuario);
                return Response<UserRegister>.Failed("Usuario não localizado");
            }
            catch (Exception ex)
            {
                return Response<UserRegister>.Failed(ex.Message);
            }
        }
        public async Task<Response<UserRegister>> InativarUsuario(UserRegister dto)
        {
            try
            {

                var retBuscarUsuario = await BuscaDiretoUsuario(dto);
                if (!retBuscarUsuario.success)
                    return retBuscarUsuario;
                retBuscarUsuario.Data.fAtivo = 0;
                return await UpdateUsuario(retBuscarUsuario.Data);
            }

            catch (Exception ex)
            {
                return Response<UserRegister>.Failed(ex.Message);
            }
        }

        public async Task<Response<PaginationTableResult<>>> ObterTodosUsuarios(PaginationTableResult<> paginacao)
        {
            try
            {

                PaginationTableResult<> modelPaginacao = new PaginationTableResult<>
                {
                    Filtro = paginacao.Filtro,
                    PaginaAtual = paginacao.PaginaAtual,
                    TamanhoPagina = paginacao.TamanhoPagina,
                    TotalPaginas = paginacao.TotalPaginas,
                    TotalRegistros = paginacao.TotalRegistros,
                };
                modelPaginacao = await _repository.Filtrar(modelPaginacao);

                paginacao.Dados = UserTable.FromList(modelPaginacao.Dados);
                paginacao.PaginaAtual = modelPaginacao.PaginaAtual;
                paginacao.TotalPaginas = modelPaginacao.TotalPaginas;
                paginacao.TamanhoPagina = modelPaginacao.TamanhoPagina;
                paginacao.TotalRegistros = modelPaginacao.TotalRegistros;

                return Response<PaginationTableResult<>>.Ok(paginacao);
            }
            catch (Exception ex)
            {
                return Response<PaginationTableResult<>>.Failed(ex.Message);
            }
        }
    }
}
