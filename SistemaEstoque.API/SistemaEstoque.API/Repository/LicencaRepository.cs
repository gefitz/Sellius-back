using Sellius.API.Context;
using Sellius.API.Models;
using Sellius.API.Repository.Interfaces;

namespace Sellius.API.Repository
{
    public class LicencaRepository : IDbMethods<LicencaModel>
    {
        private readonly AppDbContext _context;
        private readonly LogRepository _log;
        public LicencaRepository(AppDbContext context, LogRepository log)
        {
            _context = context;
            _log = log;
        }

        public Task<LicencaModel> BuscaDireto(LicencaModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Create(LicencaModel obj)
        {
            try
            {
                _context.Licencas.Add(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return false;
            }
        }

        public Task<bool> Delete(LicencaModel obj)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LicencaModel>> Filtrar(LicencaModel obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(LicencaModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
