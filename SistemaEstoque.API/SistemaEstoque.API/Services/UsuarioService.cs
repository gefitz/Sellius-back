using AutoMapper;
using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Models.Usuario;
using Sellius.API.Repository.Interfaces;
using Sellius.API.Repository.Usuarios;
using Sellius.API.Repository.Usuarios.Interfaces;

namespace Sellius.API.Services
{
    public class UsuarioService
    {
        private readonly UsuariosRepository _repository;
        private readonly LoginService _login;
        public UsuarioService(UsuariosRepository repository, LoginService login)
        {
            _repository = repository;
            _login = login;
        }
        public async Task<Response<UsuarioDTO>> CriarUsuario(UsuarioDTO dTO)
        {
            try
            {

                UsuarioModel usuario = dTO;
                if (await VereficaExistenciaUsuario(dTO)) { return Response<UsuarioDTO>.Failed("Email já esta sendo utilizado"); }
                if (await _repository.Create(usuario))
                {
                    dTO = usuario;
                    LoginDTO login = new LoginDTO
                    {
                        Email = usuario.Email
                    };


                    return Response<UsuarioDTO>.Ok(dTO);
                }
                return Response<UsuarioDTO>.Failed("Falha ao cadastrar usuario");
            }
            catch (Exception ex)
            {
                return Response<UsuarioDTO>.Failed(ex.Message);

            }

        }
        public async Task<bool> VereficaExistenciaUsuario(UsuarioDTO dto)
        {
            UsuarioModel usiario = await _repository.BuscaDiretoEmail(dto);
            if (usiario != null)
                return true;
            return false;
        }

        public async Task<Response<UsuarioDTO>> UpdateUsuario(UsuarioDTO usuario)
        {
            try
            {
                UsuarioModel model = usuario;
                if (await _repository.Update(model))
                {
                    var retLogin = await _login.AlterarSenha(usuario.login);
                    if (retLogin.success)
                    {
                        return Response<UsuarioDTO>.Ok(model);
                    }
                    else
                    {
                        return Response<UsuarioDTO>.Failed(retLogin.errorMessage);
                    }
                }
                return Response<UsuarioDTO>.Failed("Falha ao tentar fazer upadte no usuario");
            }
            catch (Exception ex)
            {
                return Response<UsuarioDTO>.Failed(ex.Message);
            }
        }

        public async Task<Response<UsuarioDTO>> BuscaDiretoUsuario(UsuarioDTO usuario)
        {
            try
            {
                usuario = await _repository.BuscaDireto(usuario);
                if (usuario != null)
                    return Response<UsuarioDTO>.Ok(usuario);
                return Response<UsuarioDTO>.Failed("Usuario não localizado");
            }
            catch (Exception ex)
            {
                return Response<UsuarioDTO>.Failed(ex.Message);
            }
        }
        public async Task<Response<UsuarioDTO>> InativarUsuario(UsuarioDTO dto)
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
                return Response<UsuarioDTO>.Failed(ex.Message);
            }
        }

        public async Task<Response<PaginacaoTabelaResult<UsuarioTabela, UsuarioFiltro>>> ObterTodosUsuarios(PaginacaoTabelaResult<UsuarioTabela, UsuarioFiltro> paginacao)
        {
            try
            {

                PaginacaoTabelaResult<UsuarioModel, UsuarioModel> modelPaginacao = new PaginacaoTabelaResult<UsuarioModel, UsuarioModel>
                {
                    Filtro = paginacao.Filtro,
                    PaginaAtual = paginacao.PaginaAtual,
                    TamanhoPagina = paginacao.TamanhoPagina,
                    TotalPaginas = paginacao.TotalPaginas,
                    TotalRegistros = paginacao.TotalRegistros,
                };
                modelPaginacao = await _repository.Filtrar(modelPaginacao);

                paginacao.Dados = UsuarioTabela.FromList(modelPaginacao.Dados);
                paginacao.PaginaAtual = modelPaginacao.PaginaAtual;
                paginacao.TotalPaginas = modelPaginacao.TotalPaginas;
                paginacao.TamanhoPagina = modelPaginacao.TamanhoPagina;
                paginacao.TotalRegistros = modelPaginacao.TotalRegistros;

                return Response<PaginacaoTabelaResult<UsuarioTabela, UsuarioFiltro>>.Ok(paginacao);
            }
            catch (Exception ex)
            {
                return Response<PaginacaoTabelaResult<UsuarioTabela, UsuarioFiltro>>.Failed(ex.Message);
            }
        }
    }
}
