using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity;
using Sellius.API.Infra.Context;
using Sellius.API.Infra.Repository.CidadeEstado.Interfaces;
using Sellius.API.Repository.Abstract;

namespace Sellius.API.Infra.Repository.CidadeEstado
{
    public class EstadoRespository(AppDbContext context) : BaseRepository<State>(context), IEstadoRepository
    {
        public async Task<bool> CreateStateAsync(State model)
        {
            DbConext.Add(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStateAsync(State model)
        {
            DbConext.Update(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<State?> FindByPredicateAsync(
            Expression<Func<State, bool>> predicate,
            Func<IQueryable<State>, IIncludableQueryable<State, object>>? include = null,
            bool asNoTracking = false)
        {
            IQueryable<State> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (asNoTracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<State>> FindAllAsync(
            Expression<Func<State, bool>> predicate,
            Func<IQueryable<State>, IIncludableQueryable<State, object>>? include = null,
            Func<IQueryable<State>, IOrderedQueryable<State>>? orderBy = null)
        {
            IQueryable<State> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (orderBy is not null)
                query = orderBy(query);
            return await query.AsNoTracking().ToListAsync();
        }
    }
}
