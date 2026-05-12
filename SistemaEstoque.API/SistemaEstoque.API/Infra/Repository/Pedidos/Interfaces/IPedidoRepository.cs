using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity.EntitysSaleOrder;

namespace Sellius.API.Infra.Repository.Pedidos.Interfaces
{
    public interface IPedidoRepository
    {
        Task<bool> CreateSaleOrderAsync(SaleOrder model);

        Task<bool> UpdateSaleOrderAsync(SaleOrder model);

        Task<SaleOrder?> FindByPredicateAsync(
            Expression<Func<SaleOrder, bool>> predicate,
            Func<IQueryable<SaleOrder>, IIncludableQueryable<SaleOrder, object>>? include = null,
            bool asNoTracking = false);

        Task<List<SaleOrder>> FindAllAsync(
            Expression<Func<SaleOrder, bool>> predicate,
            Func<IQueryable<SaleOrder>, IIncludableQueryable<SaleOrder, object>>? include = null,
            Func<IQueryable<SaleOrder>, IOrderedQueryable<SaleOrder>>? orderBy = null);
    }
}