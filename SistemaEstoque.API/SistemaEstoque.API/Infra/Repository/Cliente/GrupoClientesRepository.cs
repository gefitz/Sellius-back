using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity.EntityCustomers;
using Sellius.API.Infra.Context;
using Sellius.API.Infra.Repository.Cliente.Interfaces;
using Sellius.API.Repository.Abstract;

namespace Sellius.API.Infra.Repository.Cliente
{
    public class GrupoClientesRepository(AppDbContext context) : BaseRepository<GroupCustomer>(context), IGrupoClientesRepository
    {
        public async Task<bool> CreateGroupCustomerAsync(GroupCustomer model)
        {
            DbConext.Add(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateGroupCustomerAsync(GroupCustomer model)
        {
            DbConext.Update(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<GroupCustomer?> FindByPredicateAsync(
            Expression<Func<GroupCustomer, bool>> predicate,
            Func<IQueryable<GroupCustomer>, IIncludableQueryable<GroupCustomer, object>>? include = null,
            bool asNoTracking = false)
        {
            IQueryable<GroupCustomer> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (asNoTracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<GroupCustomer>> FindAllAsync(
            Expression<Func<GroupCustomer, bool>> predicate,
            Func<IQueryable<GroupCustomer>, IIncludableQueryable<GroupCustomer, object>>? include = null,
            Func<IQueryable<GroupCustomer>, IOrderedQueryable<GroupCustomer>>? orderBy = null)
        {
            IQueryable<GroupCustomer> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (orderBy is not null)
                query = orderBy(query);
            return await query.AsNoTracking().ToListAsync();
        }
    }
}