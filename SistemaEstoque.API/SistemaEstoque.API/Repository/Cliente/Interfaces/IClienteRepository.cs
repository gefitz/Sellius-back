using Sellius.API.DTOs.Filtros;
using Sellius.API.Models.Cliente;
using Sellius.API.Repository.Interfaces;

namespace Sellius.API.Repository.Cliente.Interfaces
{
    public interface IClienteRepository : 
        IDbMethods<ClienteModel>,
        IPaginacao<ClienteModel,FiltroCliente>
    {
    }
}
