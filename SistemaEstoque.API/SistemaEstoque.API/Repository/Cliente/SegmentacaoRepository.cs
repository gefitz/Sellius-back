using Microsoft.EntityFrameworkCore;
using Sellius.API.Context;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Models.Cliente;
using Sellius.API.Repository.Cliente.Interfaces;

namespace Sellius.API.Repository.Cliente
{
    public class SegmentacaoRepository : ISegmentacaoRepository
    {
        private readonly AppDbContext _context;

        public SegmentacaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SegmentacaoModel> BuscaDireto(SegmentacaoModel idObjeto)
        {
            try
            {
                return await _context.Segmentacaos.Where(s => s.id == idObjeto.id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<SegmentacaoModel>> CarregarCombo(int idEmpresa)
        {
            try
            {
                return await _context.Segmentacaos.Where(s => s.idEmpresa == idEmpresa).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Create(SegmentacaoModel obj)
        {
            try
            {
                _context.Segmentacaos.Add(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public Task<bool> Delete(SegmentacaoModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginacaoTabelaResult<SegmentacaoModel, SegmentacaoModel>> Filtrar(PaginacaoTabelaResult<SegmentacaoModel, SegmentacaoModel> obj)
        {
            var query = _context.Segmentacaos.AsQueryable();

            if (!string.IsNullOrEmpty(obj.Filtro.Segmento))
            {
                query = query.Where(s => s.Segmento.Contains(obj.Filtro.Segmento));
            }
            if(obj.Filtro.fAtivo != -1)
            {
                query = query.Where(s => s.fAtivo == obj.Filtro.fAtivo);
            }
            obj.TotalRegistros = query.Count();
            obj.TotalPaginas = (int)Math.Ceiling((double)obj.TotalRegistros / obj.TamanhoPagina);

            obj.Dados = await query
                .OrderBy(p => p.id)
                .Skip((obj.PaginaAtual - 1) * obj.TotalRegistros)
                .Take(obj.TamanhoPagina)
                .ToListAsync();

            return obj;
        }

        public Task<IEnumerable<SegmentacaoModel>> Filtrar(SegmentacaoModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(SegmentacaoModel obj)
        {
            try
            {
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
