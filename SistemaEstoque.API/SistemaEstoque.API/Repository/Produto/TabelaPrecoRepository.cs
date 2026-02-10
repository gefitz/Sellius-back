using Microsoft.EntityFrameworkCore;
using Sellius.API.Context;
using Sellius.API.DTOs.Filtros;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Enums;
using Sellius.API.Models.Produto;
using Sellius.API.Repository.Produto.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Sellius.API.Repository.Produto
{
    public class TabelaPrecoRepository : ITabelaPrecoRepository
    {
        private AppDbContext _context;

        public TabelaPrecoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TabelaPrecoModel> BuscaDireto(TabelaPrecoModel idObjeto)
        {
            try
            {
                return await _context.TabelaPrecos.Where(t => t.Id == idObjeto.Id && t.idEmpresa == idObjeto.idEmpresa).AsNoTracking().FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Create(TabelaPrecoModel obj)
        {
            try
            {
                _context.TabelaPrecos.Add(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> Delete(TabelaPrecoModel obj)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TabelaPrecoModel>> Filtrar(TabelaPrecoModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginacaoTabelaResult<TabelaPrecoModel, FiltroTabelaPreco>> Filtrar(PaginacaoTabelaResult<TabelaPrecoModel, FiltroTabelaPreco> obj)
        {
            FiltroTabelaPreco filtro = obj.Filtro;
            var query = _context.TabelaPrecos.AsQueryable();

            if (!string.IsNullOrEmpty(filtro.descTabelaPreco))
            {
                query = query.Where(t => t.descTabelaPreco.ToLower().Contains(filtro.descTabelaPreco.ToLower()));
            }
            if (filtro.idOrigemtabelaPreco != -1)
            {
                query = query.Where(t => t.idOrigemTabelaPreco == (OrigemTabelaPreco)filtro.idOrigemtabelaPreco);
            }
            if (filtro.dtInicialPesquisaInicioVigencia != null && filtro.dtFimPesquisaInicioVigencia != null)
            {
                query = query.Where(t =>
                t.dtInicioVigencia <= filtro.dtFimPesquisaInicioVigencia.Date
                && t.dtInicioVigencia >= filtro.dtInicialPesquisaInicioVigencia.Date);
            }
            query = query.Where(t => t.idEmpresa == filtro.idEmpresa);
            obj.TotalRegistros = query.Count();
            obj.TotalPaginas = (int)Math.Ceiling((double)obj.TotalRegistros / obj.TamanhoPagina);


            obj.Dados = await query
                .OrderBy(p => p.Id)
                .Skip(obj.PaginaAtual * obj.TotalRegistros)
                .Take(obj.TamanhoPagina)
                .ToListAsync();
            return obj;
        }

        public async Task<bool> Update(TabelaPrecoModel obj)
        {
            try
            {
                _context.TabelaPrecos.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<TabelaPrecoModel> buscaTabelaPrecoOrigemTabelaIdReferencia(TabelaPrecoModel obj)
        {
            try
            {
                return await _context.TabelaPrecos.Where(t =>
                t.idOrigemTabelaPreco == obj.idOrigemTabelaPreco
                && t.idReferenciaOrigem == obj.idReferenciaOrigem)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
