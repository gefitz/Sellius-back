using Sellius.API.Domain.Entity;
using Sellius.API.Domain.Models;
using Sellius.API.Infra.Context;
using Sellius.API.Repository.Abstract;

namespace Sellius.API.Repository
{
    public class LogRepository(AppDbContext context): BaseRepository<LogModel>(context)
    {
        public async Task Error(Exception exception)
        {
            var logModel = new LogModel
            {
                Message = exception.Message,
                InnerException = exception.InnerException?.ToString() ?? string.Empty,
                ErrorDate = DateTime.Now
            };
            DbConext.Add(logModel);
            await SaveChangesAsync();
        }
    }
}
