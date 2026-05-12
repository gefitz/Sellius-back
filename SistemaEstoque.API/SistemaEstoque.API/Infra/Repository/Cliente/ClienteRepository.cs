using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity.EntityCustomers;
using Sellius.API.Infra.Context;
using Sellius.API.Infra.Repository.Cliente.Interfaces;
using Sellius.API.Repository.Abstract;

namespace Sellius.API.Infra.Repository.Cliente
{
    public class ClienteRepository(AppDbContext context) : BaseRepository<Customer>(context), IClienteRepository
    {
        public async Task<bool> CreateCustomerAsync(Customer model)
        {
            DbConext.Add(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCustomerAsync(Customer model)
        {
            DbConext.Update(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<Customer?> FindByPredicateAsync(
            Expression<Func<Customer, bool>> predicate,
            Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>? include = null,
            bool asNoTracking = false)
        {
            IQueryable<Customer> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (asNoTracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Customer>> FindAllAsync(
            Expression<Func<Customer, bool>> predicate,
            Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>? include = null,
            Func<IQueryable<Customer>, IOrderedQueryable<Customer>>? orderBy = null)
        {
            IQueryable<Customer> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (orderBy is not null)
                query = orderBy(query);
            return await query.AsNoTracking().ToListAsync();
        }
    }
}