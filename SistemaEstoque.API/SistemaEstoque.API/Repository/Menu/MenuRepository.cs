using Microsoft.EntityFrameworkCore;
using Sellius.API.Context;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Models;
using Sellius.API.Repository.Menu.Interface;

namespace Sellius.API.Repository.Menu
{
    public class MenuRepository : IMenuRepository
    {
        private AppDbContext _context;
        private LogRepository _log;

        public MenuRepository(AppDbContext context, LogRepository log)
        {
            _context = context;
            _log = log;
        }

        public async Task<MenuModel> BuscaDireto(MenuModel obj)
        {
            try
            {
                return await _context.Menus.Where(m => m.Id == obj.Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }

        public async Task<bool> Create(MenuModel obj)
        {
            try
            {
                _context.Menus.Add(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }

        public Task<bool> Delete(MenuModel obj)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MenuModel>> Filtrar(MenuModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginacaoTabelaResult<MenuModel, MenuModel>> Filtrar(PaginacaoTabelaResult<MenuModel, MenuModel> obj)
        {
            try
            {
                var query = _context.Menus.AsQueryable();

                if (!string.IsNullOrEmpty(obj.Filtro.DeMenu))
                    query = query.Where(p => p.DeMenu.Contains(obj.Filtro.DeMenu));

                if (obj.Filtro.FAtivo)
                    query = query.Where(p => p.FAtivo == obj.Filtro.FAtivo);

                obj.TotalRegistros = query.Count();
                obj.TotalPaginas = (int)Math.Ceiling((double)obj.TotalRegistros / obj.TamanhoPagina);

                obj.Dados = await query
                    .OrderBy(p => p.Id)
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

        public async Task<bool> Update(MenuModel obj)
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
