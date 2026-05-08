using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity.Users;
using Sellius.API.Infra.Context;
using Sellius.API.Infra.Repository.Login.Interfaces;
using Sellius.API.Repository.Abstract;

namespace Sellius.API.Infra.Repository.Login
{
    public class LoginRepository(AppDbContext context) : BaseRepository<Login>(context), ILoginRepository
    {
        public async Task<bool> CreateLoginAsync(Login model)
        {
            DbConext.Add(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateLoginAsync(Login model)
        {
            DbConext.Update(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<Login?> FindByPredicateAsync(
            Expression<Func<Login, bool>> predicate,
            Func<IQueryable<Login>, IIncludableQueryable<Login, object>>? include = null,
            bool asNoTracking = false)
        {
            IQueryable<Login> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (asNoTracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Login>> FindAllAsync(
            Expression<Func<Login, bool>> predicate,
            Func<IQueryable<Login>, IIncludableQueryable<Login, object>>? include = null,
            Func<IQueryable<Login>, IOrderedQueryable<Login>>? orderBy = null)
        {
            IQueryable<Login> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (orderBy is not null)
                query = orderBy(query);
            return await query.AsNoTracking().ToListAsync();
        }
    }
}