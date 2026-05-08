using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity;
using Sellius.API.Infra.Context;
using Sellius.API.Infra.Repository.CidadeEstado.Interfaces;
using Sellius.API.Repository.Abstract;

namespace Sellius.API.Infra.Repository.CidadeEstado
{
    public class CidadeRepository(AppDbContext context) : BaseRepository<City>(context), ICidadeRepository
    {
        public async Task<bool> CreateCityAsync(City model)
        {
            DbConext.Add(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCityAsync(City model)
        {
            DbConext.Update(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<City?> FindByPredicateAsync(
            Expression<Func<City, bool>> predicate,
            Func<IQueryable<City>, IIncludableQueryable<City, object>>? include = null,
            bool asNoTracking = false)
        {
            IQueryable<City> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (asNoTracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<City>> FindAllAsync(
            Expression<Func<City, bool>> predicate,
            Func<IQueryable<City>, IIncludableQueryable<City, object>>? include = null,
            Func<IQueryable<City>, IOrderedQueryable<City>>? orderBy = null)
        {
            IQueryable<City> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (orderBy is not null)
                query = orderBy(query);
            return await query.AsNoTracking().ToListAsync();
        }
    }
}
