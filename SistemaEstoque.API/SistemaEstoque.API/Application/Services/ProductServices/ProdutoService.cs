using AutoMapper;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Domain.Models;
using Sellius.API.Domain.Models.Produto;
using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.Infra.Repository.Product;
using Sellius.API.Repository.Interfaces;
using Sellius.API.Repository.Produto;
using Sellius.API.Repository.Produto.Interface;

namespace Sellius.API.Services.Produtos
{
    public class ProdutoService
    {
        private readonly ProductRepository _repository;


        public ProdutoService(ProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<ProductRegister>> CadastrarProduto(ProductRegister productRegister)
        {
            try
            {
                Product produto = productRegister;
                if (await _repository.Create(produto))
                    return Response<ProductRegister>.Ok(produto);
                return Response<ProductRegister>.Failed("Falha ao cadastrar o produto");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        public async Task<Response<PaginationTableResult<>>> FiltrarProduto(PaginationTableResult<> paginacao)
        {
            try
            {

                PaginationTableResult<> p = new PaginationTableResult<>
                {
                    TamanhoPagina = paginacao.TamanhoPagina,
                    TotalPaginas = paginacao.TotalPaginas,
                    Filtro = paginacao.Filtro,
                    PaginaAtual = paginacao.PaginaAtual,
                };

                var paginacaoResult = await _repository.Filtrar(p);

                List<ProductTableReturn> listaProduto = ProductTableReturn.FromList(paginacaoResult.Dados);

                var result = new PaginationTableResult<>
                {
                    TamanhoPagina = paginacaoResult.TamanhoPagina,
                    TotalPaginas = paginacaoResult.TotalPaginas,
                    PaginaAtual = paginacaoResult.PaginaAtual,
                    Dados = listaProduto,
                    TotalRegistros = paginacaoResult.TotalRegistros,
                };
               return Response<PaginationTableResult<>>.Ok(result);
            }
            catch (Exception ex)
            {
                return Response<PaginationTableResult<>>.Failed(ex.Message);
            }

        }
        public async Task<Response<ProductRegister>> InativarProduto(int id)
        {
            var produtoInativar = await BuscarId(id);
            if (!produtoInativar.success)
                return produtoInativar;
            ProductRegister produto = produtoInativar.Data;
            produto.fAtivo = 0;
            return await Update(produto);

        }
        public async Task<Response<ProductRegister>> Update(ProductRegister produto)
        {
            produto.dthAlteracao = DateTime.Now;
            if (await _repository.Update(produto)) return Response<ProductRegister>.Ok();
            return Response<ProductRegister>.Failed("Falha ao tentar salvar o produto");
        }
        public async Task<Response<ProductRegister>> BuscarId(int id)
        {
            Product produto = new Product { id = id };
            produto = await _repository.BuscaDireto(produto);

            if (produto == null)
                return Response<ProductRegister>.Failed("Produto não encontrado");
            return Response<ProductRegister>.Ok(produto);
        }
        public async Task<Response<PaginationTableResult<>>> recuperarProdutosComTabelaPreco(int idEmpresa,int idCliente)
        {
            
            try
            {
                var tabelaPrecoXProduto = await _repository.recuperarProdutosComTabelaPreco(idEmpresa, idCliente);
                
                var tabelaPrecoXProdutoDTO =
                    PaginationTableResult<>
                        .RetPaginacao<PriceTableXProduct, PriceTableXProduct>(tabelaPrecoXProduto);

                tabelaPrecoXProdutoDTO.Dados =
                    PaginationTableResult<>
                        .fromList<PriceTableXProduct, PriceTableXProductTableReturn>(tabelaPrecoXProduto.Dados!,
                            dto => dto);
                
                
                return Response<PaginationTableResult<>>.Ok(tabelaPrecoXProdutoDTO);
            }
            catch (Exception e)
            {
                return  Response<PaginationTableResult<>>.Failed(e.Message);    
            }
        }
    }
}
