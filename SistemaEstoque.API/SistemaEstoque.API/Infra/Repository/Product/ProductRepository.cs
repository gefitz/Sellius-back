using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Infra.Context;
using Sellius.API.Infra.Repository.Product.Interface;
using Sellius.API.Repository.Abstract;

namespace Sellius.API.Infra.Repository.Product
{
    public class ProductRepository(AppDbContext context) :
        BaseRepository<Domain.Entity.Product.Product>(context),IProductRepository
    {
        public async Task<bool> CreateProductAsync(Domain.Entity.Product.Product model)
        {
            DbConext.Add(model);

            await SaveChangesAsync();
            
            return true;
        }

        public async Task<bool> UpdateProductAsync(Domain.Entity.Product.Product model)
        {
            DbConext.Update(model);
            
            await SaveChangesAsync();

            return true;
        }

        public async Task<Domain.Entity.Product.Product?> FindByPredicateAsync(
            Expression<Func<Domain.Entity.Product.Product, bool>> predicate,
            Func<IQueryable<Domain.Entity.Product.Product>,
                IIncludableQueryable<Domain.Entity.Product.Product, object>>? include,
            bool asNoTracking)
        {
          IQueryable<Domain.Entity.Product.Product> query = DbConext;

          query = query.Where(predicate);
          
          if(include is not null)
              query = include(query);

          if (asNoTracking)
              query = query.AsNoTracking();
          
          return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Domain.Entity.Product.Product>> FindAllAsync(Expression<Func<Domain.Entity.Product.Product, bool>> predicate, Func<IQueryable<Domain.Entity.Product.Product>, IIncludableQueryable<Domain.Entity.Product.Product, object>>? include = null, Func<IQueryable<Domain.Entity.Product.Product>, IOrderedQueryable<Domain.Entity.Product.Product>>? orderBy = null)
        {
            IQueryable<Domain.Entity.Product.Product> query =  DbConext;
            
            query = query.Where(predicate);
            
            if(include is not null)
                query = include(query);
            
            if(orderBy is not null)
                query = orderBy(query);
            
            return await query.ToListAsync();
        }
    }
}
