using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sellius.API.Infra.Context;
using Sellius.API.Infra.Repository.Product.Interface;
using Sellius.API.Repository.Abstract;

namespace Sellius.API.Infra.Repository.Product
{
    public class ProductRepository(AppDbContext context) :
        BaseRepository<Domain.Entity.EntityProduct.Product>(context),IProductRepository
    {
        public async Task<bool> CreateProductAsync(Domain.Entity.EntityProduct.Product model)
        {
            DbConext.Add(model);

            await SaveChangesAsync();
            
            return true;
        }

        public async Task<bool> UpdateProductAsync(Domain.Entity.EntityProduct.Product model)
        {
            DbConext.Update(model);
            
            await SaveChangesAsync();

            return true;
        }

        public async Task<Domain.Entity.EntityProduct.Product?> FindByPredicateAsync(
            Expression<Func<Domain.Entity.EntityProduct.Product, bool>> predicate,
            Func<IQueryable<Domain.Entity.EntityProduct.Product>,
                IIncludableQueryable<Domain.Entity.EntityProduct.Product, object>>? include,
            bool asNoTracking)
        {
          IQueryable<Domain.Entity.EntityProduct.Product> query = DbConext;

          query = query.Where(predicate);
          
          if(include is not null)
              query = include(query);

          if (asNoTracking)
              query = query.AsNoTracking();
          
          return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Domain.Entity.EntityProduct.Product>> FindAllAsync(Expression<Func<Domain.Entity.EntityProduct.Product, bool>> predicate, Func<IQueryable<Domain.Entity.EntityProduct.Product>, IIncludableQueryable<Domain.Entity.EntityProduct.Product, object>>? include = null, Func<IQueryable<Domain.Entity.EntityProduct.Product>, IOrderedQueryable<Domain.Entity.EntityProduct.Product>>? orderBy = null)
        {
            IQueryable<Domain.Entity.EntityProduct.Product> query =  DbConext;
            
            query = query.Where(predicate);
            
            if(include is not null)
                query = include(query);
            
            if(orderBy is not null)
                query = orderBy(query);
            
            return await query.ToListAsync();
        }
    }
}
