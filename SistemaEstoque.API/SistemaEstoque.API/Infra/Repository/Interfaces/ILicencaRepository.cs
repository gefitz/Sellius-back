using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity.EntityEnterprises;

namespace Sellius.API.Infra.Repository.Interfaces
{
    public interface ILicencaRepository
    {
        Task<bool> CreateLicencaAsync(License model);

        Task<bool> UpdateLicencaAsync(License model);

        Task<License?> FindByPredicateAsync(
            Expression<Func<License, bool>> predicate,
            Func<IQueryable<License>, IIncludableQueryable<License, object>>? include = null,
            bool asNoTracking = false);

        Task<List<License>> FindAllAsync(
            Expression<Func<License, bool>> predicate,
            Func<IQueryable<License>, IIncludableQueryable<License, object>>? include = null,
            Func<IQueryable<License>, IOrderedQueryable<License>>? orderBy = null);
    }
}