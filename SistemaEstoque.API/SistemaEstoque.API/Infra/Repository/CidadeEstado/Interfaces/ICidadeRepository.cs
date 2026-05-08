using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity;

namespace Sellius.API.Infra.Repository.CidadeEstado.Interfaces
{
    public interface ICidadeRepository
    {
        Task<bool> CreateCityAsync(City model);

        Task<bool> UpdateCityAsync(City model);

        Task<City?> FindByPredicateAsync(
            Expression<Func<City, bool>> predicate,
            Func<IQueryable<City>, IIncludableQueryable<City, object>>? include = null,
            bool asNoTracking = false);

        Task<List<City>> FindAllAsync(
            Expression<Func<City, bool>> predicate,
            Func<IQueryable<City>, IIncludableQueryable<City, object>>? include = null,
            Func<IQueryable<City>, IOrderedQueryable<City>>? orderBy = null);
    }
}