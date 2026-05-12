using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Domain.Entity.EntityUsers;
using Sellius.API.Domain.Models;
using Sellius.API.Infra.Context;
using Sellius.API.Infra.Repository.Users.Interfaces;
using Sellius.API.Repository.Abstract;

namespace Sellius.API.Infra.Repository.Users
{
    public class UserRepository(AppDbContext context) : BaseRepository<User>(context),IUserRepository
    {
        public async Task<bool> CreateUserAsync(User user)
        {
            DbConext.Add(user);
            
            await SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            DbConext.Update(user);
            
            await SaveChangesAsync();

            return true;
        }

        public async Task<User?> FindPredicateUserAsync(
            Expression<Func<User, bool>> predicate,
            Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null,
            bool asNoTracking = false)
        {
            IQueryable<User> query = DbConext;
            
            query = query.Where(predicate);
            
            if(include is not null)
                query = include(query);
            
            if(asNoTracking)
                query = query.AsNoTracking();
            
            return await query.FirstOrDefaultAsync();
        }

        public Task<PaginationTableResult<User>> FindAllUsers(UserFilter filter, Guid enterprise, Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null, Func<IQueryable<User>, IOrderedQueryable<User>>? order = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsUser(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
