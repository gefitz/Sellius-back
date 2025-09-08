using AutoMapper;
using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs.ClientesCadastros;
using Sellius.API.DTOs.Filtros;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Models.Cliente;
using Sellius.API.Repository.Cliente.Interfaces;
using Sellius.API.Repository.Interfaces;
using System.Runtime.InteropServices;

namespace Sellius.API.Services.Clientes
{
    public class ClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<ClienteDTO>> CadastrarCliente(ClienteDTO clienteDTO)
        {
            try
            {

                ClienteModel cliente = clienteDTO;
                if (await _repository.Create(cliente))
                    return Response<ClienteDTO>.Ok();
                return Response<ClienteDTO>.Failed("Falha ao criar o cliente");
            }
            catch (ApplicationException e)
            {
                return Response<ClienteDTO>.Failed(e.Message);
            }
            catch (Exception e)
            {
                return Response<ClienteDTO>.Failed(e.Message);

            }

        }
        public async Task<Response<PaginacaoTabelaResult<ClienteTabelaResult, FiltroCliente>>> BuscarClientes(PaginacaoTabelaResult<ClienteTabelaResult, FiltroCliente> clienteDTO)
        {
            PaginacaoTabelaResult<ClienteModel, FiltroCliente> model = new PaginacaoTabelaResult<ClienteModel, FiltroCliente>
            {
                PaginaAtual = clienteDTO.PaginaAtual,
                TamanhoPagina = clienteDTO.TamanhoPagina,
                Filtro = clienteDTO.Filtro,
                TotalPaginas = clienteDTO.TotalPaginas,
                TotalRegistros = clienteDTO.TotalRegistros,
                
            };
            model = await _repository.Filtrar(model);
            clienteDTO.Dados = ClienteTabelaResult.FromToList(model.Dados);
            clienteDTO.PaginaAtual = model.PaginaAtual;
            clienteDTO.TotalPaginas = model.TotalPaginas;
            clienteDTO.TotalRegistros = model.TotalRegistros;
            clienteDTO.TamanhoPagina = model.TamanhoPagina;
            return Response<PaginacaoTabelaResult<ClienteTabelaResult, FiltroCliente>>.Ok(clienteDTO);
        }
        public async Task<Response<ClienteDTO>> BuscarId(int id)
        {
            ClienteModel cliente = new ClienteModel { id = id };
            cliente = await _repository.BuscaDireto(cliente);
            if (cliente != null)
                return Response<ClienteDTO>.Ok(cliente);
            return Response<ClienteDTO>.Failed("Cliente não localizado");
        }
        public async Task<Response<ClienteDTO>> UpdateCliente(ClienteDTO clienteDTO)
        {
            ClienteModel cliente = clienteDTO;
            if (await _repository.Update(cliente))
                return Response<ClienteDTO>.Ok();
            return Response<ClienteDTO>.Failed("Falha ao fazer update ao cliente");
        }
        public async Task<Response<ClienteDTO>> InativarCliente(int id)
        {
            var clienteInativar = await BuscarId(id);
            if (!clienteInativar.success)
                return clienteInativar;
            ClienteModel cliente = clienteInativar.Data;
            cliente.fAtivo = 0;
            return await UpdateCliente(cliente);

        }
    }
}
