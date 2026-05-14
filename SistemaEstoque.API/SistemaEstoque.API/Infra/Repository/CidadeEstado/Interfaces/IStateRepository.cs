using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity;

namespace Sellius.API.Infra.Repository.CidadeEstado.Interfaces
{
    public interface IStateRepository
    {
        Task<bool> CreateStateAsync(State model);

        Task<bool> UpdateStateAsync(State model);

        Task<State?> FindByPredicateAsync(
            Expression<Func<State, bool>> predicate,
            Func<IQueryable<State>, IIncludableQueryable<State, object>>? include = null,
            bool asNoTracking = false);

        Task<List<State>> FindAllAsync(
            Expression<Func<State, bool>> predicate,
            Func<IQueryable<State>, IIncludableQueryable<State, object>>? include = null,
            Func<IQueryable<State>, IOrderedQueryable<State>>? orderBy = null);
    }
}