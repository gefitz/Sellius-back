using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity.EntityCustomers;

namespace Sellius.API.Infra.Repository.Cliente.Interfaces
{
    public interface IClienteRepository
    {
        Task<bool> CreateCustomerAsync(Customer model);

        Task<bool> UpdateCustomerAsync(Customer model);

        Task<Customer?> FindByPredicateAsync(
            Expression<Func<Customer, bool>> predicate,
            Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>? include = null,
            bool asNoTracking = false);

        Task<List<Customer>> FindAllAsync(
            Expression<Func<Customer, bool>> predicate,
            Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>? include = null,
            Func<IQueryable<Customer>, IOrderedQueryable<Customer>>? orderBy = null);
    }
}