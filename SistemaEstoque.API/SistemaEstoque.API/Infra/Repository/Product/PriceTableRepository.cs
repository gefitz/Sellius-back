using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity.EntityProduct;
using Sellius.API.Infra.Context;
using Sellius.API.Infra.Repository.Product.Interface;
using Sellius.API.Repository.Abstract;

namespace Sellius.API.Infra.Repository.Product
{
    public class PriceTableRepository(AppDbContext context) : BaseRepository<PriceTable>(context), IPriceTableRepository
    {
        public async Task<bool> CreatePriceTableAsync(PriceTable model)
        {
            DbConext.Add(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePriceTableAsync(PriceTable model)
        {
            DbConext.Update(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<PriceTable?> FindByPredicateAsync(
            Expression<Func<PriceTable, bool>> predicate,
            Func<IQueryable<PriceTable>, IIncludableQueryable<PriceTable, object>>? include = null,
            bool asNoTracking = false)
        {
            IQueryable<PriceTable> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (asNoTracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<PriceTable>> FindAllAsync(
            Expression<Func<PriceTable, bool>> predicate,
            Func<IQueryable<PriceTable>, IIncludableQueryable<PriceTable, object>>? include = null,
            Func<IQueryable<PriceTable>, IOrderedQueryable<PriceTable>>? orderBy = null)
        {
            IQueryable<PriceTable> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (orderBy is not null)
                query = orderBy(query);
            return await query.AsNoTracking().ToListAsync();
        }
    }
}