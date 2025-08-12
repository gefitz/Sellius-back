using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Models;
using Sellius.API.Repository;
using Sellius.API.Repository.Interfaces;
using Sellius.API.Repository.Pedidos.Interfaces;
using Sellius.API.Repository.Produto.Interface;

namespace Sellius.API.Services
{
    public class PedidoServices
    {
        private readonly IPedidoRepository _repository;
        private readonly IProdutoRepository _produtoRepository;
        public PedidoServices(IPedidoRepository repository, IProdutoRepository dbMethods)
        {
            _repository = repository;
            _produtoRepository = dbMethods;
        }

        public async Task<Response<PedidoDTO>> CadastrarPedido(PedidoDTO pedidoDTO)
        {
            try
            {

                PedidoModel pedido = pedidoDTO;
                var retPedidoXProduto = await ValidaEstoqueProduto(pedidoDTO.Produtos);
                if (!retPedidoXProduto.success)
                {
                    return Response<PedidoDTO>.Failed(retPedidoXProduto.errorMessage);
                }
                pedido.Produto = retPedidoXProduto.Data;
                if (await _repository.Create(pedido))
                    return Response<PedidoDTO>.Ok(pedido);
                return Response<PedidoDTO>.Failed("Falha ao tentar criar um pedido");
            }
            catch (Exception ex)
            {
                return Response<PedidoDTO>.Failed(ex.Message);
            }


        }
        public async Task<Response<PaginacaoTabelaResult<PedidoTabela, PedidoDTO>>> obterTodosPedidos(PaginacaoTabelaResult<PedidoTabela, PedidoDTO> filtro)
        {
            try
            {

                PaginacaoTabelaResult<PedidoModel, PedidoModel> model = new PaginacaoTabelaResult<PedidoModel, PedidoModel>
                {
                    PaginaAtual = filtro.PaginaAtual,
                    TamanhoPagina = filtro.PaginaAtual,
                    TotalPaginas = filtro.TotalPaginas,
                    TotalRegistros = filtro.TotalRegistros,
                    Filtro = filtro.Filtro
                };

                model = await _repository.Filtrar(model);

                filtro = new PaginacaoTabelaResult<PedidoTabela, PedidoDTO>
                {
                    PaginaAtual = model.PaginaAtual,
                    TamanhoPagina = model.PaginaAtual,
                    TotalPaginas = model.TotalPaginas,
                    TotalRegistros = model.TotalRegistros,
                    Dados = PedidoTabela.FromList(model.Dados)
                };
                return Response<PaginacaoTabelaResult<PedidoTabela, PedidoDTO>>.Ok(filtro);
            }
            catch (Exception ex)
            {
                return Response<PaginacaoTabelaResult<PedidoTabela, PedidoDTO>>.Failed(ex.Message);
            }
        }

        public async Task<Response<PedidoDTO>> UpdatePedido(PedidoDTO dto)
        {
            try
            {

                PedidoModel pedido = dto;
                var retPedidoXProduto = await ValidaEstoqueProduto(dto.Produtos);
                if (!retPedidoXProduto.success)
                {
                    return Response<PedidoDTO>.Failed(retPedidoXProduto.errorMessage);
                }
                pedido.Produto = retPedidoXProduto.Data;
                if (await _repository.Update(pedido))
                    return Response<PedidoDTO>.Ok(pedido);
                return Response<PedidoDTO>.Failed("Falha ao tentar criar um pedido");
            }
            catch (Exception ex)
            {
                return Response<PedidoDTO>.Failed(ex.Message);
            }

        }
        private async Task<Response<List<PedidoXProduto>>> ValidaEstoqueProduto(List<PedidoXProdutoDTO> pedidoXProdutoDTOs)
        {
            List<PedidoXProduto> pedidoXProdutos = PedidoXProduto.FromList(pedidoXProdutoDTOs);
            for (int i = 0; i > pedidoXProdutos.Count; i++)
            {
                ProdutoModel produto = await _produtoRepository.BuscaDireto(new ProdutoModel { id = pedidoXProdutos[i].idProduto });
                if (produto == null)
                {
                    return Response<List<PedidoXProduto>>.Failed("Id do produto não encontrado");
                }
                if (pedidoXProdutos[i].qtd > produto.qtd)
                {
                    return Response<List<PedidoXProduto>>.Failed($"A quantidade solicitada para o produto {produto.Nome} excede o estoque disponível. Por favor, revise o pedido e tente novamente.");
                }
                pedidoXProdutos[i].Produto = produto;
            }
            return Response<List<PedidoXProduto>>.Ok(pedidoXProdutos);

        }

    }
}
