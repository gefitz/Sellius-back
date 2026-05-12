using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity.EntityUsers;

namespace Sellius.API.Infra.Repository.Users.Interfaces
{
    public interface ITpUserRepository
    {
        Task<bool> CreateTpUserAsync(TypeUser model);
        
        Task<bool> UpdateTpUserAsync(TypeUser model);

        Task<TypeUser?> FindByPredicateAsync(Expression<Func<TypeUser, bool>> predicate,
            Func<IQueryable<TypeUser>, IIncludableQueryable<TypeUser, object>>? include,
            bool asNoTracking);
        
        Task<List<TypeUser>> FindAllAsync(
            Expression<Func<TypeUser, bool>> predicate,
            Func<IQueryable<TypeUser>, IIncludableQueryable<TypeUser, object>>? include = null,
            Func<IQueryable<TypeUser>,IOrderedQueryable<TypeUser>>?orderBy = null);
    }
}
