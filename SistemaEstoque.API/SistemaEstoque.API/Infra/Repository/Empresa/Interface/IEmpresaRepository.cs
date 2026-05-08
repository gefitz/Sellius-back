using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity.Enterprises;

namespace Sellius.API.Infra.Repository.Empresa.Interfaces
{
    public interface IEmpresaRepository
    {
        Task<bool> CreateEnterpriseAsync(Enterprise model);

        Task<bool> UpdateEnterpriseAsync(Enterprise model);

        Task<Enterprise?> FindByPredicateAsync(
            Expression<Func<Enterprise, bool>> predicate,
            Func<IQueryable<Enterprise>, IIncludableQueryable<Enterprise, object>>? include = null,
            bool asNoTracking = false);

        Task<List<Enterprise>> FindAllAsync(
            Expression<Func<Enterprise, bool>> predicate,
            Func<IQueryable<Enterprise>, IIncludableQueryable<Enterprise, object>>? include = null,
            Func<IQueryable<Enterprise>, IOrderedQueryable<Enterprise>>? orderBy = null);
    }
}