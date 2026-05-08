using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Models.Usuario;

namespace Sellius.API.Infra.Repository.Users.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<bool> CreateUser(User user);
        
        Task<bool> UpdateUser(User user);

        Task<User?> FindPredicateUser(Expression<Func<User, bool>> predicate,
            Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null,
            bool asNoTracking = false);
    }
}
