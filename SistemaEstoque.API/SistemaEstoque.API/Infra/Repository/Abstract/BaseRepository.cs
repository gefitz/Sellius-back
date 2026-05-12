using Microsoft.EntityFrameworkCore;
using Sellius.API.Infra.Context;

namespace Sellius.API.Repository.Abstract;

public class BaseRepository<T>(AppDbContext context) where T: class
{
    protected DbSet<T> DbConext = context.Set<T>();

    protected async Task SaveChangesAsync()
    {
       await context.SaveChangesAsync();
    }
    
}