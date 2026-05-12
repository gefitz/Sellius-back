using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity.EntityEnterprises;
using Sellius.API.Infra.Context;
using Sellius.API.Infra.Repository.Interfaces;
using Sellius.API.Repository.Abstract;

namespace Sellius.API.Infra.Repository
{
    public class LicencaRepository(AppDbContext context) : BaseRepository<License>(context), ILicencaRepository
    {
        public async Task<bool> CreateLicencaAsync(License model)
        {
            DbConext.Add(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateLicencaAsync(License model)
        {
            DbConext.Update(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<License?> FindByPredicateAsync(
            Expression<Func<License, bool>> predicate,
            Func<IQueryable<License>, IIncludableQueryable<License, object>>? include = null,
            bool asNoTracking = false)
        {
            IQueryable<License> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (asNoTracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<License>> FindAllAsync(
            Expression<Func<License, bool>> predicate,
            Func<IQueryable<License>, IIncludableQueryable<License, object>>? include = null,
            Func<IQueryable<License>, IOrderedQueryable<License>>? orderBy = null)
        {
            IQueryable<License> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (orderBy is not null)
                query = orderBy(query);
            return await query.AsNoTracking().ToListAsync();
        }
    }
}