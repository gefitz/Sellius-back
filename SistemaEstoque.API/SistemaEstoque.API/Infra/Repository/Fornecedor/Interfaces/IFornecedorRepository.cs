using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity;

namespace Sellius.API.Infra.Repository.Fornecedor.Interfaces
{
    public interface IFornecedorRepository
    {
        Task<bool> CreateSupplierAsync(Supplier model);

        Task<bool> UpdateSupplierAsync(Supplier model);

        Task<Supplier?> FindByPredicateAsync(
            Expression<Func<Supplier, bool>> predicate,
            Func<IQueryable<Supplier>, IIncludableQueryable<Supplier, object>>? include = null,
            bool asNoTracking = false);

        Task<List<Supplier>> FindAllAsync(
            Expression<Func<Supplier, bool>> predicate,
            Func<IQueryable<Supplier>, IIncludableQueryable<Supplier, object>>? include = null,
            Func<IQueryable<Supplier>, IOrderedQueryable<Supplier>>? orderBy = null);
    }
}