using Sellius.API.Models.Produto;
using Sellius.API.Repository.Interfaces;

namespace Sellius.API.Repository.Produto.Interface
{
    public interface ITpProdutoRepository :
        IDbMethods<TipoProdutoModel>,
        IPaginacao<TipoProdutoModel,TipoProdutoModel>
    {
        Task<List<TipoProdutoModel>> CarregarCombo(int idEmpresa);
    }
}
