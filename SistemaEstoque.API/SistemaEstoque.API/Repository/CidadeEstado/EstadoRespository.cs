using Microsoft.EntityFrameworkCore;
using Sellius.API.Context;
using Sellius.API.Models;
using Sellius.API.Repository.Interfaces;
using Sellius.API.Services;

namespace Sellius.API.Repository.CidadeEstado
{
    public class EstadoRespository : IDbMethods<EstadoModel>
    {
        private readonly AppDbContext _context;
        private readonly LogRepository _log;

        public EstadoRespository(AppDbContext context, LogRepository log)
        {
            _context = context;
            _log = log;
        }

        public Task<EstadoModel> BuscaDireto(EstadoModel idObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(EstadoModel obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(EstadoModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EstadoModel>> Filtrar(EstadoModel obj)
        {
            try
            {
                return await _context.Estados.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> Update(EstadoModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
