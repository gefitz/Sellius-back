using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity.EntityProduct;

namespace Sellius.API.Infra.Repository.Product.Interface
{
    public interface IPriceTableRepository
    {
        Task<bool> CreatePriceTableAsync(PriceTable model);

        Task<bool> UpdatePriceTableAsync(PriceTable model);

        Task<PriceTable?> FindByPredicateAsync(
            Expression<Func<PriceTable, bool>> predicate,
            Func<IQueryable<PriceTable>, IIncludableQueryable<PriceTable, object>>? include = null,
            bool asNoTracking = false);

        Task<List<PriceTable>> FindAllAsync(
            Expression<Func<PriceTable, bool>> predicate,
            Func<IQueryable<PriceTable>, IIncludableQueryable<PriceTable, object>>? include = null,
            Func<IQueryable<PriceTable>, IOrderedQueryable<PriceTable>>? orderBy = null);
    }
}