using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Sellius.API.Infra.Repository.Product.Interface
{
    public interface IProductRepository
    {
        Task<bool> CreateProductAsync(Domain.Entity.Product.Product model);
        
        Task<bool> UpdateProductAsync(Domain.Entity.Product.Product model);

        Task<Domain.Entity.Product.Product?> FindByPredicateAsync(Expression<Func<Domain.Entity.Product.Product, bool>> predicate,
            Func<IQueryable<Domain.Entity.Product.Product>, IIncludableQueryable<Domain.Entity.Product.Product, object>>? include,
            bool asNoTracking);
        
        Task<List<Domain.Entity.Product.Product>> FindAllAsync(
            Expression<Func<Domain.Entity.Product.Product, bool>> predicate,
            Func<IQueryable<Domain.Entity.Product.Product>, IIncludableQueryable<Domain.Entity.Product.Product, object>>? include = null,
            Func<IQueryable<Domain.Entity.Product.Product>,IOrderedQueryable<Domain.Entity.Product.Product>>?orderBy = null);
    }
}
