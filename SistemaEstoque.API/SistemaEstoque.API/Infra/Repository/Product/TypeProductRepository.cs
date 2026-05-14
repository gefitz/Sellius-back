using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Domain.Entity.EntityProduct;
using Sellius.API.Infra.Context;
using Sellius.API.Infra.Repository.Product.Interface;
using Sellius.API.Repository.Abstract;

namespace Sellius.API.Infra.Repository.Product
{
    public class TypeProductRepository(AppDbContext context) : BaseRepository<TypeProduct>(context), ITypeProductRepository
    {
        public async Task<bool> CreateTypeProductAsync(TypeProduct model)
        {
            DbConext.Add(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateTypeProductAsync(TypeProduct model)
        {
            DbConext.Update(model);
            await SaveChangesAsync();
            return true;
        }

        public async Task<TypeProduct?> FindByPredicateAsync(
            Expression<Func<TypeProduct, bool>> predicate,
            Func<IQueryable<TypeProduct>, IIncludableQueryable<TypeProduct, object>>? include = null,
            bool asNoTracking = false)
        {
            IQueryable<TypeProduct> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (asNoTracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<TypeProduct>> FindAllAsync(
            Expression<Func<TypeProduct, bool>> predicate,
            Func<IQueryable<TypeProduct>, IIncludableQueryable<TypeProduct, object>>? include = null,
            Func<IQueryable<TypeProduct>, IOrderedQueryable<TypeProduct>>? orderBy = null)
        {
            IQueryable<TypeProduct> query = DbConext.Where(predicate);
            if (include is not null)
                query = include(query);
            if (orderBy is not null)
                query = orderBy(query);
            return await query.AsNoTracking().ToListAsync();
        }
    }
}