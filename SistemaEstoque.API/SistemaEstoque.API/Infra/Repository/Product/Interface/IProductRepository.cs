using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Sellius.API.Infra.Repository.Product.Interface
{
    public interface IProductRepository
    {
        Task<bool> CreateProductAsync(Domain.Entity.EntityProduct.Product model);
        
        Task<bool> UpdateProductAsync(Domain.Entity.EntityProduct.Product model);

        Task<Domain.Entity.EntityProduct.Product?> FindByPredicateAsync(Expression<Func<Domain.Entity.EntityProduct.Product, bool>> predicate,
            Func<IQueryable<Domain.Entity.EntityProduct.Product>, IIncludableQueryable<Domain.Entity.EntityProduct.Product, object>>? include,
            bool asNoTracking);
        
        Task<List<Domain.Entity.EntityProduct.Product>> FindAllAsync(
            Expression<Func<Domain.Entity.EntityProduct.Product, bool>> predicate,
            Func<IQueryable<Domain.Entity.EntityProduct.Product>, IIncludableQueryable<Domain.Entity.EntityProduct.Product, object>>? include = null,
            Func<IQueryable<Domain.Entity.EntityProduct.Product>,IOrderedQueryable<Domain.Entity.EntityProduct.Product>>?orderBy = null);
    }
}
