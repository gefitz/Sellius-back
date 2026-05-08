using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity.Users;

namespace Sellius.API.Infra.Repository.Login.Interfaces
{
    public interface ILoginRepository
    {
        Task<bool> CreateLoginAsync(Login model);

        Task<bool> UpdateLoginAsync(Login model);

        Task<Login?> FindByPredicateAsync(
            Expression<Func<Login, bool>> predicate,
            Func<IQueryable<Login>, IIncludableQueryable<Login, object>>? include = null,
            bool asNoTracking = false);

        Task<List<Login>> FindAllAsync(
            Expression<Func<Login, bool>> predicate,
            Func<IQueryable<Login>, IIncludableQueryable<Login, object>>? include = null,
            Func<IQueryable<Login>, IOrderedQueryable<Login>>? orderBy = null);
    }
}