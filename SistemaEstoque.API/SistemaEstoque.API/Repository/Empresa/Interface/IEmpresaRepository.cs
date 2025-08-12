using Sellius.API.Models;
using Sellius.API.Repository.Interfaces;

namespace Sellius.API.Repository.Empresa.Interface
{
    public interface IEmpresaRepository:
        IDbMethods<EmpresaModel>,
        IPaginacao<EmpresaModel, EmpresaModel>
    {
    }
}
