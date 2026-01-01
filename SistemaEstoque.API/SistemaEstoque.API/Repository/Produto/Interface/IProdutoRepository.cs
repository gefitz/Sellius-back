using Sellius.API.DTOs.Filtros;
using Sellius.API.Models.Produto;
using Sellius.API.Repository.Interfaces;

namespace Sellius.API.Repository.Produto.Interface
{
    public interface IProdutoRepository
        :
        IDbMethods<ProdutoModel>,
        IPaginacao<ProdutoModel,FiltroProduto>
    {
    }
}
