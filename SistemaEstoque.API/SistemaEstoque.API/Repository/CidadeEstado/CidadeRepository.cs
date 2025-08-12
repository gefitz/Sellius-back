using Microsoft.EntityFrameworkCore;
using Sellius.API.Context;
using Sellius.API.Models;
using Sellius.API.Repository.Interfaces;

namespace Sellius.API.Repository.CidadeEstado
{
    public class CidadeRepository : IDbMethods<CidadeModel>
    {
        private readonly AppDbContext _context;
        private readonly LogRepository _log;

        public CidadeRepository(AppDbContext context, LogRepository log)
        {
            _context = context;
            _log = log;
        }

        public Task<CidadeModel> BuscaDireto(CidadeModel idObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(CidadeModel obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(CidadeModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CidadeModel>> Filtrar(CidadeModel obj)
        {
            try
            {
                return await _context.Cidades.Include(e=>e.Estado).Where(c => c.EstadoId == obj.EstadoId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> Update(CidadeModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
