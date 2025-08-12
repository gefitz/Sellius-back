using AutoMapper;
using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Models;
using Sellius.API.Repository.Interfaces;
using Sellius.API.Repository.Produto;
using Sellius.API.Repository.Produto.Interface;

namespace Sellius.API.Services.Produtos
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _repository;


        public ProdutoService(IProdutoRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<ProdutoDTO>> CadastrarProduto(ProdutoDTO produtoDTO)
        {
            ProdutoModel produto = produtoDTO;
            if (await _repository.Create(produto))
                return Response<ProdutoDTO>.Ok();
            return Response<ProdutoDTO>.Failed("Falha ao cadastrar o produto");
        }
        public async Task<Response<PaginacaoTabelaResult<ProdutoTabela, FiltroProduto>>> FiltrarProduto(PaginacaoTabelaResult<ProdutoDTO, FiltroProduto> paginacao)
        {
            try
            {

                PaginacaoTabelaResult<ProdutoModel, FiltroProduto> p = new PaginacaoTabelaResult<ProdutoModel, FiltroProduto>
                {
                    TamanhoPagina = paginacao.TamanhoPagina,
                    TotalPaginas = paginacao.TotalPaginas,
                    Filtro = paginacao.Filtro,
                    PaginaAtual = paginacao.PaginaAtual,
                };

                var paginacaoResult = await _repository.Filtrar(p);

                List<ProdutoTabela> listaProduto = ProdutoTabela.FromList(paginacaoResult.Dados);

                var result = new PaginacaoTabelaResult<ProdutoTabela, FiltroProduto>
                {
                    TamanhoPagina = paginacaoResult.TamanhoPagina,
                    TotalPaginas = paginacaoResult.TotalPaginas,
                    PaginaAtual = paginacaoResult.PaginaAtual,
                    Dados = listaProduto,
                    TotalRegistros = paginacaoResult.TotalRegistros,
                };
               return Response<PaginacaoTabelaResult<ProdutoTabela, FiltroProduto>>.Ok(result);
            }
            catch (Exception ex)
            {
                return Response<PaginacaoTabelaResult<ProdutoTabela, FiltroProduto>>.Failed(ex.Message);
            }

        }
        public async Task<Response<ProdutoDTO>> InativarProduto(int id)
        {
            var produtoInativar = await BuscarId(id);
            if (!produtoInativar.success)
                return produtoInativar;
            ProdutoDTO produto = produtoInativar.Data;
            produto.fAtivo = 0;
            return await Update(produto);

        }
        public async Task<Response<ProdutoDTO>> Update(ProdutoDTO produto)
        {
            produto.dthAlteracao = DateTime.Now;
            if (await _repository.Update(produto)) return Response<ProdutoDTO>.Ok();
            return Response<ProdutoDTO>.Failed("Falha ao tentar salvar o produto");
        }
        public async Task<Response<ProdutoDTO>> BuscarId(int id)
        {
            ProdutoModel produto = new ProdutoModel { id = id };
            produto = await _repository.BuscaDireto(produto);

            if (produto == null)
                return Response<ProdutoDTO>.Failed("Produto não encontrado");
            return Response<ProdutoDTO>.Ok(produto);
        }
    }
}
