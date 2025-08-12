using Sellius.API.Models;
using Sellius.API.Repository.Interfaces;

namespace Sellius.API.Repository.Fornecedor.Interfaces
{
    public interface IFornecedorRepository:
        IDbMethods<FornecedoresModel>,
        IPaginacao<FornecedoresModel,FornecedoresModel>
    {
        public Task<List<FornecedoresModel>> CarregarComboFornecedor(FornecedoresModel model);
    }
}
