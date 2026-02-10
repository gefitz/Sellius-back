using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Models.Produto;
using Sellius.API.Models.Usuario;
using Sellius.API.Repository.Produto;

namespace Sellius.API.Services.Produtos
{
    public class TabelaPrecoService
    {
        private TabelaPrecoRepository _repository;

        public TabelaPrecoService(TabelaPrecoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<TabelaPrecoDTO>> criarTabela(TabelaPrecoDTO tabelaDTO)
        {
            try
            {


                TabelaPrecoModel tabelaExistente = await _repository.buscaTabelaPrecoOrigemTabelaIdReferencia(tabelaDTO);
                if (tabelaExistente != null)
                {
                    tabelaExistente.dtFimVigencia = DateTime.UtcNow;
                    var respUpdate = await UpDateTabelaPreco(tabelaExistente);
                    if (!respUpdate.success) { return respUpdate; }
                }
                tabelaDTO.dtCadastro = DateTime.UtcNow;
                tabelaDTO.dtAtualizado = DateTime.UtcNow;

                if (await _repository.Create(tabelaDTO))
                {
                    return Response<TabelaPrecoDTO>.Ok();
                }
                return Response<TabelaPrecoDTO>.Failed("Falha ao tentar criar a tabela de preco");
            }
            catch (Exception ex) { return Response<TabelaPrecoDTO>.Failed(ex.Message); }
        }
        public async Task<Response<TabelaPrecoDTO>> UpDateTabelaPreco(TabelaPrecoDTO tabelaDTO)
        {
            tabelaDTO.dtAtualizado = DateTime.UtcNow;
            if (await _repository.Update(tabelaDTO))
            {
                return Response<TabelaPrecoDTO>.Ok(tabelaDTO);
            }
            return Response<TabelaPrecoDTO>.Failed("Falha ao salvar alteracao da tabela de preco");
        }
        public async Task<Response<TabelaPrecoDTO>> InativarTabelaPreco(TabelaPrecoDTO dto)
        {
            try
            {

                var retBuscarUsuario = await BuscaDireto(dto);
                if (!retBuscarUsuario.success)
                    return retBuscarUsuario;
                retBuscarUsuario.Data.dtFimVigencia = DateTime.UtcNow;
                return await UpDateTabelaPreco(retBuscarUsuario.Data);
            }

            catch (Exception ex)
            {
                return Response<TabelaPrecoDTO>.Failed(ex.Message);
            }
        }

        public async Task<Response<PaginacaoTabelaResult<TabelaPrecoDTO, FiltroTabelaPreco>>> ObterTodasTabelasPreco(PaginacaoTabelaResult<TabelaPrecoDTO, FiltroTabelaPreco> paginacao)
        {
            try
            {

                PaginacaoTabelaResult<TabelaPrecoModel, FiltroTabelaPreco> modelPaginacao = new PaginacaoTabelaResult<TabelaPrecoModel, FiltroTabelaPreco>
                {
                    Filtro = paginacao.Filtro,
                    PaginaAtual = paginacao.PaginaAtual,
                    TamanhoPagina = paginacao.TamanhoPagina,
                    TotalPaginas = paginacao.TotalPaginas,
                    TotalRegistros = paginacao.TotalRegistros,
                };
                modelPaginacao = await _repository.Filtrar(modelPaginacao);
                paginacao.Dados = TabelaPrecoDTO.FromList(modelPaginacao.Dados);

                paginacao.PaginaAtual = modelPaginacao.PaginaAtual;
                paginacao.TotalPaginas = modelPaginacao.TotalPaginas;
                paginacao.TamanhoPagina = modelPaginacao.TamanhoPagina;
                paginacao.TotalRegistros = modelPaginacao.TotalRegistros;

                return Response<PaginacaoTabelaResult<TabelaPrecoDTO, FiltroTabelaPreco>>.Ok(paginacao);
            }
            catch (Exception ex)
            {
                return Response<PaginacaoTabelaResult<TabelaPrecoDTO, FiltroTabelaPreco>>.Failed(ex.Message);
            }
        }
        public async Task<Response<TabelaPrecoDTO>> BuscaDireto(TabelaPrecoDTO usuario)
        {
            try
            {
                usuario = await _repository.BuscaDireto(usuario);
                if (usuario != null)
                    return Response<TabelaPrecoDTO>.Ok(usuario);
                return Response<TabelaPrecoDTO>.Failed("Usuario não localizado");
            }
            catch (Exception ex)
            {
                return Response<TabelaPrecoDTO>.Failed(ex.Message);
            }
        }
    }
}
