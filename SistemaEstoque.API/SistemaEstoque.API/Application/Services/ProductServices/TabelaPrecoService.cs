using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Domain.Models;
using Sellius.API.Domain.Models.Produto;
using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;
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

        public async Task<Response<PriceTableRegister>> criarTabela(PriceTableRegister tabelaDTO)
        {
            try
            {


                PriceTable tabelaExistente = await _repository.buscaTabelaPrecoOrigemTabelaIdReferencia(tabelaDTO);
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
                    return Response<PriceTableRegister>.Ok();
                }
                return Response<PriceTableRegister>.Failed("Falha ao tentar criar a tabela de preco");
            }
            catch (Exception ex) { return Response<PriceTableRegister>.Failed(ex.Message); }
        }
        public async Task<Response<PriceTableRegister>> UpDateTabelaPreco(PriceTableRegister tabelaDTO)
        {
            tabelaDTO.dtAtualizado = DateTime.UtcNow;
            if (await _repository.Update(tabelaDTO))
            {
                return Response<PriceTableRegister>.Ok(tabelaDTO);
            }
            return Response<PriceTableRegister>.Failed("Falha ao salvar alteracao da tabela de preco");
        }
        public async Task<Response<PriceTableRegister>> InativarTabelaPreco(PriceTableRegister dto)
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
                return Response<PriceTableRegister>.Failed(ex.Message);
            }
        }

        public async Task<Response<PaginationTableResult<>>> ObterTodasTabelasPreco(PaginationTableResult<> paginacao)
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
                paginacao.Dados = PriceTableRegister.FromList(modelPaginacao.Dados);

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
        public async Task<Response<PriceTableRegister>> BuscaDireto(PriceTableRegister usuario)
        {
            try
            {
                usuario = await _repository.BuscaDireto(usuario);
                if (usuario != null)
                    return Response<PriceTableRegister>.Ok(usuario);
                return Response<PriceTableRegister>.Failed("Usuario não localizado");
            }
            catch (Exception ex)
            {
                return Response<PriceTableRegister>.Failed(ex.Message);
            }
        }
    }
}
