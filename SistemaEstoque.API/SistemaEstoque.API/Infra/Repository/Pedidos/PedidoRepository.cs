using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity.EntitysSaleOrder;
using Sellius.API.Infra.Context;
using Sellius.API.Infra.Repository.Pedidos.Interfaces;
using Sellius.API.Repository.Abstract;

namespace Sellius.API.Infra.Repository.Pedidos
{
    public class PedidoRepository(AppDbContext context) : BaseRepository<SaleOrder>(context), IPedidoRepository
    {
        public async Task<bool> CreateSaleOrderAsync(SaleOrder model)
        {
            DbConext.Add(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateSaleOrderAsync(SaleOrder model)
        {
            DbConext.Update(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<SaleOrder?> FindByPredicateAsync(
            Expression<Func<SaleOrder, bool>> predicate,
            Func<IQueryable<SaleOrder>, IIncludableQueryable<SaleOrder, object>>? include = null,
            bool asNoTracking = false)
        {
            IQueryable<SaleOrder> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (asNoTracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<SaleOrder>> FindAllAsync(
            Expression<Func<SaleOrder, bool>> predicate,
            Func<IQueryable<SaleOrder>, IIncludableQueryable<SaleOrder, object>>? include = null,
            Func<IQueryable<SaleOrder>, IOrderedQueryable<SaleOrder>>? orderBy = null)
        {
            IQueryable<SaleOrder> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (orderBy is not null)
                query = orderBy(query);
            return await query.AsNoTracking().ToListAsync();
        }
    }
}