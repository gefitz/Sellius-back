using AutoMapper;
using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Models;
using Sellius.API.Repository.Interfaces;
using Sellius.API.Repository.Usuarios.Interfaces;

namespace Sellius.API.Services
{
    public class UsuarioService
    {
        private readonly IUsuariosRepository _repository;

        public UsuarioService(IUsuariosRepository repository)
        {
            _repository = repository;
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
            UsuarioModel usiario = await _repository.BuscaDireto(dto);
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
                    return Response<UsuarioDTO>.Ok(model);
                }
                return Response<UsuarioDTO>.Failed("Falha ao tentar fazer upadte no usuario");
            }
            catch (Exception ex)
            {
                return Response<UsuarioDTO>.Failed(ex.Message);
            }
        }

        public async Task<Response<UsuarioDTO>> BuscaDiretoUsuario(int id)
        {
            try
            {
                UsuarioModel usuario = new UsuarioModel { id = id };
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
        public async Task<Response<UsuarioDTO>> InativarUsuario(int id)
        {
            try
            {
                var retBuscarUsuario = await BuscaDiretoUsuario(id);
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

        public async Task<Response<PaginacaoTabelaResult<UsuarioDTO, UsuarioDTO>>> ObterTodosUsuarios(PaginacaoTabelaResult<UsuarioDTO, UsuarioDTO> paginacao)
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
                
                paginacao.Dados = UsuarioDTO.FromList(modelPaginacao.Dados);

                return Response<PaginacaoTabelaResult<UsuarioDTO, UsuarioDTO>>.Ok(paginacao);
            }
            catch (Exception ex)
            {
                return Response<PaginacaoTabelaResult<UsuarioDTO, UsuarioDTO>>.Failed(ex.Message);
            }
        }
    }
}
