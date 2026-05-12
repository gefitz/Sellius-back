using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity.EntityUsers;
using Sellius.API.Infra.Context;
using Sellius.API.Infra.Repository.Users.Interfaces;
using Sellius.API.Repository.Abstract;

namespace Sellius.API.Infra.Repository.Users
{
    public class TpUserRepository(AppDbContext context) : BaseRepository<TypeUser>(context),ITpUserRepository
    {
        public async Task<bool> CreateTpUserAsync(TypeUser model)
        {
            DbConext.Add(model);

            await SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateTpUserAsync(TypeUser model)
        {
            DbConext.Update(model);
            
            await SaveChangesAsync();

            return true;
        }

        public async Task<TypeUser?> FindByPredicateAsync(
            Expression<Func<TypeUser, bool>> predicate,
            Func<IQueryable<TypeUser>, IIncludableQueryable<TypeUser, object>>? include,
            bool asNoTracking)
        {
            IQueryable<TypeUser> query = DbConext.Where(predicate);

            if(include is not null)
                query = include(query);
            
            if (asNoTracking)
                query = query.AsNoTracking();
            
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<TypeUser>> FindAllAsync(
            Expression<Func<TypeUser, bool>> predicate,
            Func<IQueryable<TypeUser>, IIncludableQueryable<TypeUser, object>>? include = null, 
            Func<IQueryable<TypeUser>, IOrderedQueryable<TypeUser>>? orderBy = null)
        {
            IQueryable<TypeUser> query = DbConext;

            if (include is not null)
                query = include(query);
            
            if(orderBy is not null)
                query = orderBy(query);
            
            query = query.AsNoTracking();

            return await query.ToListAsync();
        }
    }
}
