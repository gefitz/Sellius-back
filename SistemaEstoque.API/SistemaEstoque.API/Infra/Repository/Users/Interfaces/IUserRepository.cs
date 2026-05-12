using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Domain.Entity.EntityUsers;
using Sellius.API.Domain.Models;

namespace Sellius.API.Infra.Repository.Users.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> CreateUserAsync(User user);
        
        Task<bool> UpdateUserAsync(User user);

        Task<User?> FindPredicateUserAsync(Expression<Func<User, bool>> predicate,
            Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null,
            bool asNoTracking = false);
        Task<PaginationTableResult<User>> FindAllUsers(UserFilter filter,Guid enterprise,
            Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null,
            Func<IQueryable<User>, IOrderedQueryable<User>>? order = null);
        
        Task<bool> ExistsUser(Expression<Func<User, bool>> predicate);
    }
}
