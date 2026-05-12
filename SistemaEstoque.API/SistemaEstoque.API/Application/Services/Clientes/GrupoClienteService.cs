using Sellius.API.Application.DTOs.RegisterDTOs.CustomerRegisterDTOs;
using Sellius.API.Domain.Entity.EntityCustomers;
using Sellius.API.Domain.Models;
using Sellius.API.DTOs;
using Sellius.API.Repository.Cliente;
using Sellius.API.Repository.Cliente.Interfaces;

namespace Sellius.API.Services.Clientes
{
    public class GrupoClienteService
    {
        private readonly GrupoClientesRepository _repository;

        public GrupoClienteService(GrupoClientesRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<GroupCustomerRegister>> CadastrarGrupo(GroupCustomerRegister groupCustomerRegister)
        {
            try
            {

                GroupCustomer grupo = groupCustomerRegister;
                if (await _repository.Create(grupo))
                    return Response<GroupCustomerRegister>.Ok();
                return Response<GroupCustomerRegister>.Failed("Falha ao criar o grupo");
            }
            catch (ApplicationException e)
            {
                return Response<GroupCustomerRegister>.Failed(e.Message);
            }
            catch (Exception e)
            {
                return Response<GroupCustomerRegister>.Failed(e.Message);

            }

        }
        public async Task<Response<PaginationTableResult<>>> BuscarClientes(PaginationTableResult<> grupoClienteDTO)
        {
            PaginationTableResult<> model = new PaginationTableResult<>
            {
                PaginaAtual = grupoClienteDTO.PaginaAtual,
                TamanhoPagina = grupoClienteDTO.TamanhoPagina,
                Filtro = grupoClienteDTO.Filtro,
                TotalPaginas = grupoClienteDTO.TotalPaginas,
                TotalRegistros = grupoClienteDTO.TotalRegistros,

            };
            model = await _repository.Filtrar(model);

            grupoClienteDTO = PaginationTableResult<>.RetPaginacao(model);


            grupoClienteDTO.Dados = GroupCustomerRegister.FromToList(model.Dados);
            return Response<PaginationTableResult<>>.Ok(grupoClienteDTO);
        }
        public async Task<Response<GroupCustomerRegister>> BuscarId(int id)
        {
            GroupCustomer grupo = new GroupCustomer { id = id };
            grupo = await _repository.BuscaDireto(grupo);
            if (grupo != null)
                return Response<GroupCustomerRegister>.Ok(grupo);
            return Response<GroupCustomerRegister>.Failed("Cliente não localizado");
        }
        public async Task<Response<GroupCustomerRegister>> UpdateGrupo(GroupCustomerRegister groupCustomerRegister)
        {
            GroupCustomer grupo = groupCustomerRegister;
            if (await _repository.Update(grupo))
                return Response<GroupCustomerRegister>.Ok();
            return Response<GroupCustomerRegister>.Failed("Falha ao fazer update ao grupo");
        }
        public async Task<Response<GroupCustomerRegister>> InativarCliente(int id)
        {

            var grupo = await BuscarId(id);
            if (!grupo.success)
                return grupo;
            GroupCustomer model = grupo.Data;
            model.fAtivo = 0;
            model.dthAlteracao = DateTime.Now;
            if (await _repository.Delete(model))
            {
                return Response<GroupCustomerRegister>.Ok();
            }
            return Response<GroupCustomerRegister>.Failed("Falha ao inativar o grupo");

        }
        public async Task<Response<List<GroupCustomerRegister>>> CarregarCombo(int idEmpresa)
        {
            var grupo = GroupCustomerRegister.FromToList(await _repository.CarregarCombo(idEmpresa));

            return Response<List<GroupCustomerRegister>>.Ok(grupo);
        }
    }
}
