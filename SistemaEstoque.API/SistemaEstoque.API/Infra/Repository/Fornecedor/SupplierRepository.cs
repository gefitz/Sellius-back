using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity;
using Sellius.API.Infra.Context;
using Sellius.API.Infra.Repository.Fornecedor.Interfaces;
using Sellius.API.Repository.Abstract;

namespace Sellius.API.Infra.Repository.Fornecedor
{
    public class SupplierRepository(AppDbContext context) : BaseRepository<Supplier>(context), ISupplierRepository
    {
        public async Task<bool> CreateSupplierAsync(Supplier model)
        {
            DbConext.Add(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateSupplierAsync(Supplier model)
        {
            DbConext.Update(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<Supplier?> FindByPredicateAsync(
            Expression<Func<Supplier, bool>> predicate,
            Func<IQueryable<Supplier>, IIncludableQueryable<Supplier, object>>? include = null,
            bool asNoTracking = false)
        {
            IQueryable<Supplier> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (asNoTracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Supplier>> FindAllAsync(
            Expression<Func<Supplier, bool>> predicate,
            Func<IQueryable<Supplier>, IIncludableQueryable<Supplier, object>>? include = null,
            Func<IQueryable<Supplier>, IOrderedQueryable<Supplier>>? orderBy = null)
        {
            IQueryable<Supplier> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (orderBy is not null)
                query = orderBy(query);
            return await query.AsNoTracking().ToListAsync();
        }
    }
}