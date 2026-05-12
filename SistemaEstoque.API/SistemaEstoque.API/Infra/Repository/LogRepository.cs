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
            LogModel logModel = new LogModel();
                logModel.Messagem = exception.Message;
                logModel.InnerExecption = exception.InnerException!.Message;
                if (exception.InnerException != null)
                    logModel.InnerExecption = exception.InnerException.ToString();

                logModel.dthErro = DateTime.Now;
                DbConext.Add(logModel);
                await SaveChangesAsync();
        }
    }
}
