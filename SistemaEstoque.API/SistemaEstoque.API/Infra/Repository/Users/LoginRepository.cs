using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity.EntityUsers;
using Sellius.API.Infra.Context;
using Sellius.API.Infra.Repository.Users.Interfaces;
using Sellius.API.Repository.Abstract;

namespace Sellius.API.Infra.Repository.Users
{
    public class LoginRepository(AppDbContext context)
        : BaseRepository<Authentication>(context), ILoginRepository
    {
        public async Task<bool> CreateLoginAsync(Authentication model)
        {
            DbConext.Add(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateLoginAsync(Authentication model)
        {
            DbConext.Update(model);
            await SaveChangesAsync();
            return true;
        }
        
        public async Task<Authentication?> FindByPredicateAsync(
            Expression<Func<Authentication, bool>> predicate,
            Func<IQueryable<Authentication>
                , IIncludableQueryable<Authentication, object>>? include = null,
            bool asNoTracking = false)
        {
            IQueryable<Authentication> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (asNoTracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Authentication>> FindAllAsync(
            Expression<Func<Authentication, bool>> predicate,
            Func<IQueryable<Authentication>, IIncludableQueryable<Authentication, object>>? include = null,
            Func<IQueryable<Authentication>, IOrderedQueryable<Authentication>>? orderBy = null)
        {
            IQueryable<Authentication> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (orderBy is not null)
                query = orderBy(query);
            return await query.AsNoTracking().ToListAsync();
        }
    }
}