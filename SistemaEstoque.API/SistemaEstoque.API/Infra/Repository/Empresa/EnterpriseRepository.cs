using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity.EntityEnterprises;
using Sellius.API.Infra.Context;
using Sellius.API.Infra.Repository.Empresa.Interfaces;
using Sellius.API.Repository.Abstract;

namespace Sellius.API.Infra.Repository.Empresa
{
    public class EnterpriseRepository(AppDbContext context) : BaseRepository<Enterprise>(context), IEnterpriseRepository
    {
        public async Task<bool> CreateEnterpriseAsync(Enterprise model)
        {
            DbConext.Add(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateEnterpriseAsync(Enterprise model)
        {
            DbConext.Update(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<Enterprise?> FindByPredicateAsync(
            Expression<Func<Enterprise, bool>> predicate,
            Func<IQueryable<Enterprise>, IIncludableQueryable<Enterprise, object>>? include = null,
            bool asNoTracking = false)
        {
            IQueryable<Enterprise> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (asNoTracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Enterprise>> FindAllAsync(
            Expression<Func<Enterprise, bool>> predicate,
            Func<IQueryable<Enterprise>, IIncludableQueryable<Enterprise, object>>? include = null,
            Func<IQueryable<Enterprise>, IOrderedQueryable<Enterprise>>? orderBy = null)
        {
            IQueryable<Enterprise> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (orderBy is not null)
                query = orderBy(query);
            return await query.AsNoTracking().ToListAsync();
        }
    }
}