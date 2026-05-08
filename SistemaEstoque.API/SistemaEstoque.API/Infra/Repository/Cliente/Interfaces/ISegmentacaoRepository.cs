using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity.Customers;

namespace Sellius.API.Infra.Repository.Cliente.Interfaces
{
    public interface ISegmentacaoRepository
    {
        Task<bool> CreateSegmentationAsync(Segmentation model);

        Task<bool> UpdateSegmentationAsync(Segmentation model);

        Task<Segmentation?> FindByPredicateAsync(
            Expression<Func<Segmentation, bool>> predicate,
            Func<IQueryable<Segmentation>, IIncludableQueryable<Segmentation, object>>? include = null,
            bool asNoTracking = false);

        Task<List<Segmentation>> FindAllAsync(
            Expression<Func<Segmentation, bool>> predicate,
            Func<IQueryable<Segmentation>, IIncludableQueryable<Segmentation, object>>? include = null,
            Func<IQueryable<Segmentation>, IOrderedQueryable<Segmentation>>? orderBy = null);
    }
}