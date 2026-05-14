using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity.EntityCustomers;
using Sellius.API.Infra.Context;
using Sellius.API.Infra.Repository.Cliente.Interfaces;
using Sellius.API.Repository.Abstract;

namespace Sellius.API.Infra.Repository.Cliente
{
    public class SegmentationRepository(AppDbContext context) : BaseRepository<Segmentation>(context), ISegmentationRepository
    {
        public async Task<bool> CreateSegmentationAsync(Segmentation model)
        {
            DbConext.Add(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateSegmentationAsync(Segmentation model)
        {
            DbConext.Update(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<Segmentation?> FindByPredicateAsync(
            Expression<Func<Segmentation, bool>> predicate,
            Func<IQueryable<Segmentation>, IIncludableQueryable<Segmentation, object>>? include = null,
            bool asNoTracking = false)
        {
            IQueryable<Segmentation> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (asNoTracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Segmentation>> FindAllAsync(
            Expression<Func<Segmentation, bool>> predicate,
            Func<IQueryable<Segmentation>, IIncludableQueryable<Segmentation, object>>? include = null,
            Func<IQueryable<Segmentation>, IOrderedQueryable<Segmentation>>? orderBy = null)
        {
            IQueryable<Segmentation> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (orderBy is not null)
                query = orderBy(query);
            return await query.AsNoTracking().ToListAsync();
        }
    }
}