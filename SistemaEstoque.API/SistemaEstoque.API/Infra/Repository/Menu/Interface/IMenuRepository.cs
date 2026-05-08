using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity.Users;

namespace Sellius.API.Infra.Repository.Menu.Interfaces
{
    public interface IMenuRepository
    {
        Task<bool> CreateMenuAsync(Menu model);

        Task<bool> UpdateMenuAsync(Menu model);

        Task<Menu?> FindByPredicateAsync(
            Expression<Func<Menu, bool>> predicate,
            Func<IQueryable<Menu>, IIncludableQueryable<Menu, object>>? include = null,
            bool asNoTracking = false);

        Task<List<Menu>> FindAllAsync(
            Expression<Func<Menu, bool>> predicate,
            Func<IQueryable<Menu>, IIncludableQueryable<Menu, object>>? include = null,
            Func<IQueryable<Menu>, IOrderedQueryable<Menu>>? orderBy = null);
    }
}