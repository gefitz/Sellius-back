using Sellius.API.Models.Cliente;
using Sellius.API.Repository.Interfaces;

namespace Sellius.API.Repository.Cliente.Interfaces
{
    public interface IGrupoClientesRepository : IDbMethods<GrupoClienteModel>, IPaginacao<GrupoClienteModel, GrupoClienteModel>
    {
        Task<List<GrupoClienteModel>> CarregarCombo(int idEmpresa);
    }
}
