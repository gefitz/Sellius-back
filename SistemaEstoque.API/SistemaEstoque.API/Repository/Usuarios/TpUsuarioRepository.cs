using Microsoft.EntityFrameworkCore;
using Sellius.API.Context;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Models.Usuario;
using Sellius.API.Repository.Usuarios.Interfaces;
using System.Threading.Tasks;

namespace Sellius.API.Repository.Usuarios
{
    public class TpUsuarioRepository : ITpUsuarioRepository
    {
        private AppDbContext _context;
        private LogRepository _log;

        public TpUsuarioRepository(AppDbContext context, LogRepository log)
        {
            _context = context;
            _log = log;
        }

        public async Task<TpUsuarioModel> BuscaDireto(TpUsuarioModel obj)
        {
            try
            {
                return await _context.TpUsuarios.Where(m => m.id == obj.id).AsNoTracking().FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }

        public async Task<bool> Create(TpUsuarioModel obj)
        {
            try
            {
                _context.TpUsuarios.Add(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }

        public async Task<bool> Delete(TpUsuarioModel obj)
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }

        public Task<IEnumerable<TpUsuarioModel>> Filtrar(TpUsuarioModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginacaoTabelaResult<TpUsuarioModel, TpUsuarioModel>> Filtrar(PaginacaoTabelaResult<TpUsuarioModel, TpUsuarioModel> obj)
        {
            try
            {
                var query = _context.TpUsuarios.AsQueryable();

                if (!string.IsNullOrEmpty(obj.Filtro.tpUsuario))
                    query = query.Where(p => p.tpUsuario.ToLower().Contains(obj.Filtro.tpUsuario.ToLower()));

                if (obj.Filtro.fAtivo != -1)
                    query = query.Where(p => p.fAtivo == obj.Filtro.fAtivo);

                obj.TotalRegistros = query.Count();
                obj.TotalPaginas = (int)Math.Ceiling((double)obj.TotalRegistros / obj.TamanhoPagina);

                obj.Dados = await query
                    .Include(t => t.tpUsuarioXMenus)
                    .ThenInclude(m => m.Menu)
                    .Include(t => t.TpUsuarioConfigurcao)
                    .OrderBy(p => p.id)
                    .Skip(obj.PaginaAtual * obj.TamanhoPagina)
                    .Take(obj.TamanhoPagina)
                    .ToListAsync();


                return obj;

            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }

        public async Task<List<TpUsuarioModel>> recuperaTpUsuarios(int idEmpresa)
        {
            try
            {
                return await _context.TpUsuarios.Where(m => m.fAtivo == 1 && (m.idEmpresa == 0 || m.idEmpresa == idEmpresa)).ToListAsync();

            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }

        public async Task<bool> Update(TpUsuarioModel obj)
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

        public async Task<List<TpUsuarioXMenu>> obterTodosTpUsuarioXMenu(int idTpUsuario)
        {
            try
            {
                return await _context.TpUsuariosXMenus.Where(tpx => tpx.idTpUsuario == idTpUsuario).ToListAsync();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }

        public async Task RemoverTodosTpUsuariosXMenu(List<TpUsuarioXMenu> usuarioXMenus)
        {
            try
            {
                _context.TpUsuariosXMenus.RemoveRange(usuarioXMenus);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }

        public async Task AdicionarTodosTpUsuariosXMenu(List<TpUsuarioXMenu> usuarioXMenus)
        {
            try
            {
                await _context.TpUsuariosXMenus.AddRangeAsync(usuarioXMenus);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }

        public async Task<TpUsuarioConfiguracao> obterConfiguracao(int idTpUsuario)
        {
            try
            {
                return await _context.TpUsuarioConfiguracaos.Where(tpx => tpx.idTpUsuario == idTpUsuario).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }

        public async Task RemoverConfiguracao(TpUsuarioConfiguracao usuarioXMenus)
        {
            try
            {
                _context.TpUsuarioConfiguracaos.RemoveRange(usuarioXMenus);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }

        public async Task AdicionarConfiguracao(TpUsuarioConfiguracao usuarioXMenus)
        {
            try
            {
                await _context.TpUsuarioConfiguracaos.AddRangeAsync(usuarioXMenus);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }

        public async Task<List<TpUsuarioModel>>obterTodosTpUsuarios(int idEmpresa)
        {
            return await _context.TpUsuarios.Where(c => c.idEmpresa == idEmpresa).ToListAsync();
        }
    }
}
