using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity.EntityUsers;

namespace Sellius.API.Infra.Repository.Users.Interfaces
{
    public interface ILoginRepository
    {
        Task<bool> CreateLoginAsync(Authentication model);

        Task<bool> UpdateLoginAsync(Authentication model);

        Task<Authentication?> FindByPredicateAsync(
            Expression<Func<Authentication, bool>> predicate,
            Func<IQueryable<Authentication>, IIncludableQueryable<Authentication, object>>? include = null,
            bool asNoTracking = false);

        Task<List<Authentication>> FindAllAsync(
            Expression<Func<Authentication, bool>> predicate,
            Func<IQueryable<Authentication>, IIncludableQueryable<Authentication, object>>? include = null,
            Func<IQueryable<Authentication>, IOrderedQueryable<Authentication>>? orderBy = null);
    }
}