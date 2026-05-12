using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity.EntityUsers;
using Sellius.API.Infra.Context;
using Sellius.API.Infra.Repository.Users.Interfaces;
using Sellius.API.Repository.Abstract;

namespace Sellius.API.Infra.Repository.Users
{
    public class MenuRepository(AppDbContext context) : BaseRepository<Menu>(context), IMenuRepository
    {
        public async Task<bool> CreateMenuAsync(Menu model)
        {
            DbConext.Add(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMenuAsync(Menu model)
        {
            DbConext.Update(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<Menu?> FindByPredicateAsync(
            Expression<Func<Menu, bool>> predicate,
            Func<IQueryable<Menu>, IIncludableQueryable<Menu, object>>? include = null,
            bool asNoTracking = false)
        {
            IQueryable<Menu> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            
            if (asNoTracking)
                query = query.AsNoTracking();
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Menu>> FindAllAsync(
            Expression<Func<Menu, bool>> predicate,
            Func<IQueryable<Menu>, IIncludableQueryable<Menu, object>>? include = null,
            Func<IQueryable<Menu>, IOrderedQueryable<Menu>>? orderBy = null)
        {
            IQueryable<Menu> query = DbConext.Where(predicate);
            
            if (include is not null)
                query = include(query);
            
            if (orderBy is not null)
                query = orderBy(query);
            
            return await query.AsNoTracking().ToListAsync();
        }
    }
}
