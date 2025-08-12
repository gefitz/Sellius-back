using Sellius.API.Context;
using Sellius.API.Models;

namespace Sellius.API.Repository
{
    public class LogRepository
    {
        private readonly AppDbContext _context;

        public LogRepository(AppDbContext context)
        {
            _context = context;
            
        }

        public void Error(string message, bool SalvarLog)
        {
            LogModel logModel = new LogModel();
            if (!string.IsNullOrEmpty(message)) { logModel.Messagem = message; }
            if (SalvarLog)
            {
                logModel.InnerExecption = "";
                logModel.dthErro = DateTime.Now;
                _context.Logs.Add(logModel);
                _context.SaveChanges();
            }
            logModel.Messagem = message;
            

        }
        public void Error(Exception exception)
        {
            LogModel logModel = new LogModel();

            if (exception != null)
            {
                logModel.Messagem = exception.Message;
                logModel.InnerExecption = "";
                if (exception.InnerException != null)
                    logModel.InnerExecption = exception.InnerException.ToString();

                logModel.dthErro = DateTime.Now;
                _context.Logs.Add(logModel);
                _context.SaveChanges();
                logModel.Messagem = exception.Message;
            }
        }
    }
}
