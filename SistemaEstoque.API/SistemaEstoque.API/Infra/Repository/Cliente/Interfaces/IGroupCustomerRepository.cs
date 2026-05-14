using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity.EntityCustomers;

namespace Sellius.API.Infra.Repository.Cliente.Interfaces
{
    public interface IGroupCustomerRepository
    {
        Task<bool> CreateGroupCustomerAsync(GroupCustomer model);

        Task<bool> UpdateGroupCustomerAsync(GroupCustomer model);

        Task<GroupCustomer?> FindByPredicateAsync(
            Expression<Func<GroupCustomer, bool>> predicate,
            Func<IQueryable<GroupCustomer>, IIncludableQueryable<GroupCustomer, object>>? include = null,
            bool asNoTracking = false);

        Task<List<GroupCustomer>> FindAllAsync(
            Expression<Func<GroupCustomer, bool>> predicate,
            Func<IQueryable<GroupCustomer>, IIncludableQueryable<GroupCustomer, object>>? include = null,
            Func<IQueryable<GroupCustomer>, IOrderedQueryable<GroupCustomer>>? orderBy = null);
    }
}