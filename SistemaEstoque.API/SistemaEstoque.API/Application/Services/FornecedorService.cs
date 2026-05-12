using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Domain.Entity;
using Sellius.API.Domain.Models;
using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.Models;
using Sellius.API.Repository.Fornecedor;
using Sellius.API.Repository.Fornecedor.Interfaces;

namespace Sellius.API.Services
{
    public class FornecedorService
    {
        private readonly FornecedorRepository _repository;

        public FornecedorService(FornecedorRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<SupplierRegister>> CadastrarFornecedor(SupplierRegister dto)
        {
            dto.fAtivo = 1;
            Supplier model = dto;
            if (await _repository.Create(model))
                return Response<SupplierRegister>.Ok(model);
            return Response<SupplierRegister>.Failed("Falha ao criar um novo tipo produto");

        }
        public async Task<Response<PaginationTableResult<>>> BuscarFornecedor(PaginationTableResult<> dto)
        {
            try
            {

                PaginationTableResult<> model = new PaginationTableResult<>
                {
                    TotalRegistros = dto.TotalRegistros,
                    TotalPaginas = dto.TotalPaginas,
                    TamanhoPagina = dto.TamanhoPagina,
                    Filtro = dto.Filtro,
                    PaginaAtual = dto.PaginaAtual

                };
                var result = await _repository.Filtrar(model);
                dto.Dados = SupplierTableReturn.FromList(result.Dados);
                return Response<PaginationTableResult<>>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Response<PaginationTableResult<>>.Failed(ex.Message);
            }
        }
        public async Task<Response<SupplierRegister>> BuscarId(int id)
        {
            try
            {
                Supplier cliente = new Supplier { id = id };
                return Response<SupplierRegister>.Ok(await _repository.BuscaDireto(cliente));
            }
            catch (Exception ex)
            {
                return Response<SupplierRegister>.Failed(ex.Message);
            }
        }
        public async Task<Response<SupplierRegister>> UpdateFornecedor(SupplierRegister dto)
        {

            var fornecedorOriginal = await BuscarId(dto.id);
            if (!fornecedorOriginal.success)
                return Response<SupplierRegister>.Failed("Fornecedor não foi encontrado");


            Supplier model = dto;
            model.dthAlteracao = DateTime.UtcNow;
            model.dthCadastro = (DateTime)fornecedorOriginal.Data.dthCadastro;
            if (fornecedorOriginal.Data.cidade != null)
                model.Cidade = fornecedorOriginal.Data.cidade;

            if (await _repository.Update(model))
                return Response<SupplierRegister>.Ok(model);
            return Response<SupplierRegister>.Failed("Falha ao fazer modificação");
        }
        public async Task<Response<SupplierRegister>> InativarFornecedor(SupplierRegister dto)
        {
            var model = await BuscarId(dto.id);
            if (!model.success)
            {
                return Response<SupplierRegister>.Failed("O id desse tipo produto não foi encontrado");
            }
            return await UpdateFornecedor(dto);
        }
        public async Task<Response<List<SupplierRegister>>> CarregarComboFornecedor(SupplierRegister fornecedor)
        {
            var fornecedoRet = await _repository.CarregarComboFornecedor(fornecedor);
            return Response<List<SupplierRegister>>.Ok(SupplierRegister.FromList(fornecedoRet));
        }
        public async Task<Response<SupplierXCustomerTableReturn>> AddFornecedorXCliente(SupplierXCustomerTableReturn supplierXCliente)
        {
            if (_repository.addFornecedorXCliente(supplierXCliente))
            {
                return Response<SupplierXCustomerTableReturn>.Ok();
            }
            return Response<SupplierXCustomerTableReturn>.Failed("Falha ao tenta criar um vinculo com fonecedor ao cliente");
        }

        public async Task<Response<PaginationTableResult<>>> ObterFornecedorXClientePaginada(PaginationTableResult<> fornecedorXCliente)
        {
            try
            {
                fornecedorXCliente = await _repository.obterFornecedorXClientePaginada(fornecedorXCliente);
                return Response<PaginationTableResult<>>.Ok(fornecedorXCliente);
            }catch (Exception ex) {
                
                return Response<PaginationTableResult<>>.Failed(ex.Message);
            }
        }

        public async Task<Response<SupplierXCustomerTableReturn>> removerVinculoFornecedorXCliente(SupplierXCustomerTableReturn supplierXCliente)
        {
            if(await _repository.removerVinculoFornecedorXCliente(supplierXCliente))
            {
                return Response<SupplierXCustomerTableReturn>.Ok();
            }
            return Response<SupplierXCustomerTableReturn>.Failed("Falha ao tentar remover o vinculo");
        } 
    }
}
