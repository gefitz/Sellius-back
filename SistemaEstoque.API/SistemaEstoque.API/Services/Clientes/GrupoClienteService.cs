using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs.ClientesCadastros;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Models.Cliente;
using Sellius.API.Repository.Cliente.Interfaces;

namespace Sellius.API.Services.Clientes
{
    public class GrupoClienteService
    {
        private readonly IGrupoClientesRepository _repository;

        public GrupoClienteService(IGrupoClientesRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<GrupoClienteDTO>> CadastrarGrupo(GrupoClienteDTO grupoClienteDTO)
        {
            try
            {

                GrupoClienteModel grupo = grupoClienteDTO;
                if (await _repository.Create(grupo))
                    return Response<GrupoClienteDTO>.Ok();
                return Response<GrupoClienteDTO>.Failed("Falha ao criar o grupo");
            }
            catch (ApplicationException e)
            {
                return Response<GrupoClienteDTO>.Failed(e.Message);
            }
            catch (Exception e)
            {
                return Response<GrupoClienteDTO>.Failed(e.Message);

            }

        }
        public async Task<Response<PaginacaoTabelaResult<GrupoClienteDTO, GrupoClienteDTO>>> BuscarClientes(PaginacaoTabelaResult<GrupoClienteDTO, GrupoClienteDTO> grupoClienteDTO)
        {
            PaginacaoTabelaResult<GrupoClienteModel, GrupoClienteModel> model = new PaginacaoTabelaResult<GrupoClienteModel, GrupoClienteModel>
            {
                PaginaAtual = grupoClienteDTO.PaginaAtual,
                TamanhoPagina = grupoClienteDTO.TamanhoPagina,
                Filtro = grupoClienteDTO.Filtro,
                TotalPaginas = grupoClienteDTO.TotalPaginas,
                TotalRegistros = grupoClienteDTO.TotalRegistros,

            };
            model = await _repository.Filtrar(model);

            grupoClienteDTO = PaginacaoTabelaResult<GrupoClienteDTO, GrupoClienteDTO>.RetPaginacao(model);


            grupoClienteDTO.Dados = GrupoClienteDTO.FromToList(model.Dados);
            return Response<PaginacaoTabelaResult<GrupoClienteDTO, GrupoClienteDTO>>.Ok(grupoClienteDTO);
        }
        public async Task<Response<GrupoClienteDTO>> BuscarId(int id)
        {
            GrupoClienteModel grupo = new GrupoClienteModel { id = id };
            grupo = await _repository.BuscaDireto(grupo);
            if (grupo != null)
                return Response<GrupoClienteDTO>.Ok(grupo);
            return Response<GrupoClienteDTO>.Failed("Cliente não localizado");
        }
        public async Task<Response<GrupoClienteDTO>> UpdateGrupo(GrupoClienteDTO GrupoClienteDTO)
        {
            GrupoClienteModel grupo = GrupoClienteDTO;
            if (await _repository.Update(grupo))
                return Response<GrupoClienteDTO>.Ok();
            return Response<GrupoClienteDTO>.Failed("Falha ao fazer update ao grupo");
        }
        public async Task<Response<GrupoClienteDTO>> InativarCliente(int id)
        {

            var grupo = await BuscarId(id);
            if (!grupo.success)
                return grupo;
            GrupoClienteModel model = grupo.Data;
            model.fAtivo = 0;
            model.dthAlteracao = DateTime.Now;
            if (await _repository.Delete(model))
            {
                return Response<GrupoClienteDTO>.Ok();
            }
            return Response<GrupoClienteDTO>.Failed("Falha ao inativar o grupo");

        }
    }
}
