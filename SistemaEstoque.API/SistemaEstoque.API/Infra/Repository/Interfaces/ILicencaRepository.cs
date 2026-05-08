using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Models.Enterprise;

namespace Sellius.API.Infra.Repository.Interfaces
{
    public interface ILicencaRepository
    {
        Task<bool> CreateLicencaAsync(LicencaModel model);

        Task<bool> UpdateLicencaAsync(LicencaModel model);

        Task<LicencaModel?> FindByPredicateAsync(
            Expression<Func<LicencaModel, bool>> predicate,
            Func<IQueryable<LicencaModel>, IIncludableQueryable<LicencaModel, object>>? include = null,
            bool asNoTracking = false);

        Task<List<LicencaModel>> FindAllAsync(
            Expression<Func<LicencaModel, bool>> predicate,
            Func<IQueryable<LicencaModel>, IIncludableQueryable<LicencaModel, object>>? include = null,
            Func<IQueryable<LicencaModel>, IOrderedQueryable<LicencaModel>>? orderBy = null);
    }
}