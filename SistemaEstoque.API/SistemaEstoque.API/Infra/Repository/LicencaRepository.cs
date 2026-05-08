using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Models.Enterprise;
using Sellius.API.Infra.Context;
using Sellius.API.Infra.Repository.Interfaces;
using Sellius.API.Repository.Abstract;

namespace Sellius.API.Infra.Repository
{
    public class LicencaRepository(AppDbContext context) : BaseRepository<LicencaModel>(context), ILicencaRepository
    {
        public async Task<bool> CreateLicencaAsync(LicencaModel model)
        {
            DbConext.Add(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateLicencaAsync(LicencaModel model)
        {
            DbConext.Update(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<LicencaModel?> FindByPredicateAsync(
            Expression<Func<LicencaModel, bool>> predicate,
            Func<IQueryable<LicencaModel>, IIncludableQueryable<LicencaModel, object>>? include = null,
            bool asNoTracking = false)
        {
            IQueryable<LicencaModel> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (asNoTracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<LicencaModel>> FindAllAsync(
            Expression<Func<LicencaModel, bool>> predicate,
            Func<IQueryable<LicencaModel>, IIncludableQueryable<LicencaModel, object>>? include = null,
            Func<IQueryable<LicencaModel>, IOrderedQueryable<LicencaModel>>? orderBy = null)
        {
            IQueryable<LicencaModel> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (orderBy is not null)
                query = orderBy(query);
            return await query.AsNoTracking().ToListAsync();
        }
    }
}