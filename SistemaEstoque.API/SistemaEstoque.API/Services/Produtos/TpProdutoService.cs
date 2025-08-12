using AutoMapper;
using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Models;
using Sellius.API.Repository.Interfaces;
using Sellius.API.Repository.Produto.Interface;

namespace Sellius.API.Services.Produtos
{
    public class TpProdutoService
    {
        private readonly ITpProdutoRepository _repository;

        public TpProdutoService(ITpProdutoRepository repository, IMapper mapper)
        {
            _repository = repository;
        }
        public async Task<Response<TipoProdutoDTO>> CadastrarTpProduto(TipoProdutoDTO dto)
        {
            TipoProdutoDTO model = dto;
            if (await _repository.Create(model))
                return Response<TipoProdutoDTO>.Ok(model);
            return Response<TipoProdutoDTO>.Failed("Falha ao criar um novo tipo produto");

        }
        public async Task<Response<PaginacaoTabelaResult<TipoProdutoDTO, TipoProdutoDTO>>> BuscarTpProudo(PaginacaoTabelaResult<TipoProdutoDTO, TipoProdutoDTO> dto)
        {
            try
            {

                PaginacaoTabelaResult<TipoProdutoModel, TipoProdutoModel> model = new PaginacaoTabelaResult<TipoProdutoModel, TipoProdutoModel>
                {
                    TotalRegistros = dto.TotalRegistros,
                    TotalPaginas = dto.TotalPaginas,
                    TamanhoPagina = dto.TamanhoPagina,
                    Filtro = dto.Filtro,
                    PaginaAtual = dto.PaginaAtual

                };
                var result = await _repository.Filtrar(model);
                dto.Dados = TipoProdutoDTO.FromList(result.Dados);
                dto.TotalRegistros = result.TotalRegistros;
                return Response<PaginacaoTabelaResult<TipoProdutoDTO, TipoProdutoDTO>>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Response<PaginacaoTabelaResult<TipoProdutoDTO, TipoProdutoDTO>>.Failed(ex.Message);
            }
        }
        public async Task<Response<TipoProdutoDTO>> BuscarId(int id)
        {
            try
            {
                TipoProdutoModel tp = new TipoProdutoModel { id = id };
                return Response<TipoProdutoDTO>.Ok(await _repository.BuscaDireto(tp));
            }
            catch (Exception ex)
            {
                return Response<TipoProdutoDTO>.Failed(ex.Message);
            }
        }
        public async Task<Response<TipoProdutoDTO>> UpdateTpProduto(TipoProdutoDTO dto)
        {
            TipoProdutoModel model = dto;
            if(await _repository.Update(model))
                return Response<TipoProdutoDTO>.Ok(model);
            return Response<TipoProdutoDTO>.Failed("Falha ao fazer modificação");
        }
        public async Task<Response<TipoProdutoDTO>> InativarTpProduto(int id)
        {
            var model = await BuscarId(id);
            if (!model.success) {
                return Response<TipoProdutoDTO>.Failed("O id desse tipo produto não foi encontrado");
            }
            TipoProdutoModel tp = model.Data;
            tp.fAtivo = 0;
            if(await _repository.Delete(tp))
            {
                return Response<TipoProdutoDTO>.Ok(tp);
            }
            return Response<TipoProdutoDTO>.Failed("Falha ao inativar o tipo produto");
        }
        public async Task<Response<List<TipoProdutoDTO>>> CarregarCombo(int idEmpresa)
        {
            List<TipoProdutoDTO> listTp = TipoProdutoDTO.FromList(await _repository.CarregarCombo(idEmpresa));
            return Response<List<TipoProdutoDTO>>.Ok(listTp);
        }
    }
}
