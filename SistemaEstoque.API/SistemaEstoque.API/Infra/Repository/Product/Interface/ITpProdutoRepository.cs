using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity.Product;

namespace Sellius.API.Infra.Repository.Product.Interface
{
    public interface ITpProdutoRepository
    {
        Task<bool> CreateTypeProductAsync(TypeProduct model);

        Task<bool> UpdateTypeProductAsync(TypeProduct model);

        Task<TypeProduct?> FindByPredicateAsync(
            Expression<Func<TypeProduct, bool>> predicate,
            Func<IQueryable<TypeProduct>, IIncludableQueryable<TypeProduct, object>>? include = null,
            bool asNoTracking = false);

        Task<List<TypeProduct>> FindAllAsync(
            Expression<Func<TypeProduct, bool>> predicate,
            Func<IQueryable<TypeProduct>, IIncludableQueryable<TypeProduct, object>>? include = null,
            Func<IQueryable<TypeProduct>, IOrderedQueryable<TypeProduct>>? orderBy = null);
    }
}