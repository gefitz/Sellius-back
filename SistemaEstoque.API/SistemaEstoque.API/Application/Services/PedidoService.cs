using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Domain.Entity.EntitysSaleOrder;
using Sellius.API.Domain.Models;
using Sellius.API.Domain.Models.Pedido;
using Sellius.API.Domain.Models.Produto;
using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.Infra.Repository.Product;
using Sellius.API.Repository;
using Sellius.API.Repository.Interfaces;
using Sellius.API.Repository.Pedidos;
using Sellius.API.Repository.Pedidos.Interfaces;
using Sellius.API.Repository.Produto;
using Sellius.API.Repository.Produto.Interface;

namespace Sellius.API.Services
{
    public class PedidoService
    {
        private readonly PedidoRepository _repository;
        private readonly ProductRepository _productRepository;
        public PedidoService(PedidoRepository repository, ProductRepository dbMethods)
        {
            _repository = repository;
            _productRepository = dbMethods;
        }

        public async Task<Response<SaleOrderRegister>> CadastrarPedido(SaleOrderRegister saleOrderRegister)
        {
            try
            {

                SaleOrder pedido = saleOrderRegister;
                var retPedidoXProduto = await ValidaEstoqueProduto(saleOrderRegister.Produtos);
                if (!retPedidoXProduto.success)
                {
                    return Response<SaleOrderRegister>.Failed(retPedidoXProduto.errorMessage);
                }
                pedido.Produto = retPedidoXProduto.Data;
                if (await _repository.Create(pedido))
                    return Response<SaleOrderRegister>.Ok(pedido);
                return Response<SaleOrderRegister>.Failed("Falha ao tentar criar um pedido");
            }
            catch (Exception ex)
            {
                return Response<SaleOrderRegister>.Failed(ex.Message);
            }


        }
        public async Task<Response<PaginationTableResult<>>> obterTodosPedidos(PaginationTableResult<> filtro)
        {
            try
            {

                PaginationTableResult<> model = new PaginationTableResult<>
                {
                    PaginaAtual = filtro.PaginaAtual,
                    TamanhoPagina = filtro.PaginaAtual,
                    TotalPaginas = filtro.TotalPaginas,
                    TotalRegistros = filtro.TotalRegistros,
                    Filtro = filtro.Filtro
                };

                model = await _repository.Filtrar(model);

                filtro = new PaginationTableResult<>
                {
                    PaginaAtual = model.PaginaAtual,
                    TamanhoPagina = model.PaginaAtual,
                    TotalPaginas = model.TotalPaginas,
                    TotalRegistros = model.TotalRegistros,
                    Dados = SaleOrderTableReturn.FromList(model.Dados)
                };
                return Response<PaginationTableResult<>>.Ok(filtro);
            }
            catch (Exception ex)
            {
                return Response<PaginationTableResult<>>.Failed(ex.Message);
            }
        }

        public async Task<Response<SaleOrderRegister>> UpdatePedido(SaleOrderRegister dto)
        {
            try
            {

                SaleOrder pedido = dto;
                var retPedidoXProduto = await ValidaEstoqueProduto(dto.Produtos);
                if (!retPedidoXProduto.success)
                {
                    return Response<SaleOrderRegister>.Failed(retPedidoXProduto.errorMessage);
                }
                pedido.Produto = retPedidoXProduto.Data;
                if (await _repository.Update(pedido))
                    return Response<SaleOrderRegister>.Ok(pedido);
                return Response<SaleOrderRegister>.Failed("Falha ao tentar criar um pedido");
            }
            catch (Exception ex)
            {
                return Response<SaleOrderRegister>.Failed(ex.Message);
            }

        }
        private async Task<Response<List<SaleOrdeXProduct>>> ValidaEstoqueProduto(List<SaleOrderXProductRegister> pedidoXProdutoDTOs)
        {
            List<SaleOrdeXProduct> pedidoXProdutos = SaleOrdeXProduct.FromList(pedidoXProdutoDTOs);
            for (int i = 0; i > pedidoXProdutos.Count; i++)
            {
                Product produto = await _productRepository.BuscaDireto(new Product { id = pedidoXProdutos[i].idProduto });
                if (produto == null)
                {
                    return Response<List<SaleOrdeXProduct>>.Failed("Id do produto não encontrado");
                }
                if (pedidoXProdutos[i].qtd > produto.qtd)
                {
                    return Response<List<SaleOrdeXProduct>>.Failed($"A quantidade solicitada para o produto {produto.Nome} excede o estoque disponível. Por favor, revise o pedido e tente novamente.");
                }
                pedidoXProdutos[i].Produto = produto;
            }
            return Response<List<SaleOrdeXProduct>>.Ok(pedidoXProdutos);

        }

    }
}
