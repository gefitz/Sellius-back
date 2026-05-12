using AutoMapper;
using Sellius.API.DTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.Repository.Cliente;
using Sellius.API.Repository.Cliente.Interfaces;
using Sellius.API.Repository.Interfaces;
using System.Runtime.InteropServices;
using Sellius.API.Application.DTOs.RegisterDTOs.CustomerRegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Domain.Entity.EntityCustomers;
using Sellius.API.Domain.Models;

namespace Sellius.API.Services.Clientes
{
    public class ClienteService
    {
        private readonly ClienteRepository _repository;

        public ClienteService(ClienteRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<CustomerRegister>> CadastrarCliente(CustomerRegister customerRegister)
        {
            try
            {

                Customer cliente = customerRegister;
                if (await _repository.Create(cliente))
                    return Response<CustomerRegister>.Ok();
                return Response<CustomerRegister>.Failed("Falha ao criar o cliente");
            }
            catch (ApplicationException e)
            {
                return Response<CustomerRegister>.Failed(e.Message);
            }
            catch (Exception e)
            {
                return Response<CustomerRegister>.Failed(e.Message);

            }

        }
        public async Task<Response<PaginationTableResult<>>> BuscarClientes(PaginationTableResult<> clienteDTO)
        {
            PaginationTableResult<> model = new PaginationTableResult<>
            {
                PaginaAtual = clienteDTO.PaginaAtual,
                TamanhoPagina = clienteDTO.TamanhoPagina,
                Filtro = clienteDTO.Filtro,
                TotalPaginas = clienteDTO.TotalPaginas,
                TotalRegistros = clienteDTO.TotalRegistros,
                
            };
            model = await _repository.Filtrar(model);
            clienteDTO.Dados = CustomerTableReturn.FromToList(model.Dados);
            clienteDTO.PaginaAtual = model.PaginaAtual;
            clienteDTO.TotalPaginas = model.TotalPaginas;
            clienteDTO.TotalRegistros = model.TotalRegistros;
            clienteDTO.TamanhoPagina = model.TamanhoPagina;
            return Response<PaginationTableResult<>>.Ok(clienteDTO);
        }
        public async Task<Response<CustomerRegister>> BuscarId(int id)
        {
            Customer cliente = new Customer { id = id };
            cliente = await _repository.BuscaDireto(cliente);
            if (cliente != null)
                return Response<CustomerRegister>.Ok(cliente);
            return Response<CustomerRegister>.Failed("Cliente não localizado");
        }
        public async Task<Response<CustomerRegister>> UpdateCliente(CustomerRegister customerRegister)
        {
            Customer cliente = customerRegister;
            if (await _repository.Update(cliente))
                return Response<CustomerRegister>.Ok();
            return Response<CustomerRegister>.Failed("Falha ao fazer update ao cliente");
        }
        public async Task<Response<CustomerRegister>> InativarCliente(int id)
        {
            var clienteInativar = await BuscarId(id);
            if (!clienteInativar.success)
                return clienteInativar;
            Customer cliente = clienteInativar.Data;
            cliente.fAtivo = 0;
            return await UpdateCliente(cliente);

        }
    }
}
