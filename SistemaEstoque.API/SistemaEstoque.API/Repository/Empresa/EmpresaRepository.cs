using Microsoft.EntityFrameworkCore;
using Sellius.API.Context;
using Sellius.API.DTOs;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Models;
using Sellius.API.Repository.Empresa.Interface;
using Sellius.API.Repository.Interfaces;

namespace Sellius.API.Repository.Empresa
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private AppDbContext _context;
        private LogRepository _log;

        public EmpresaRepository(AppDbContext context, LogRepository log)
        {
            _context = context;
            _log = log;
        }

        public async Task<EmpresaModel> BuscaDireto(EmpresaModel obj)
        {
            try
            {
                return await _context.Empresas.Where(e => e.CNPJ.Equals(obj.CNPJ)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }

        public async Task<bool> Create(EmpresaModel obj)
        {
            try
            {
                _context.Empresas.Add(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }

        public Task<bool> Delete(EmpresaModel obj)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmpresaModel>> Filtrar(EmpresaModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginacaoTabelaResult<EmpresaModel, EmpresaModel>> Filtrar(PaginacaoTabelaResult<EmpresaModel, EmpresaModel> obj)
        {
            try
            {
                var query = _context.Empresas.AsQueryable();

                if (!string.IsNullOrEmpty(obj.Filtro.Nome))
                    query = query.Where(p => p.Nome.Contains(obj.Filtro.Nome));
                if (!string.IsNullOrEmpty(obj.Filtro.CNPJ))
                    query = query.Where(p => p.CNPJ.Contains(obj.Filtro.CNPJ));
                if (obj.Filtro.fAtivo != 0)
                    query = query.Where(p => p.fAtivo.Equals(obj.Filtro.fAtivo));
                if (obj.Filtro.CidadeId != 0)
                    query = query.Where(p => p.CidadeId.Equals(obj.Filtro.CidadeId));

                obj.TotalRegistros = query.Count();
                obj.TotalPaginas = (int)Math.Ceiling((double)obj.TotalRegistros / obj.TamanhoPagina);

                obj.Dados = await query
                    .OrderBy(p => p.id)
                    .Skip((obj.PaginaAtual - 1) * obj.TotalRegistros)
                    .Take(obj.TamanhoPagina)
                    .ToListAsync();

                return obj;

            }
            catch(Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }

        public async Task<bool> Update(EmpresaModel obj)
        {
            try
            {

                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }
    }
}
