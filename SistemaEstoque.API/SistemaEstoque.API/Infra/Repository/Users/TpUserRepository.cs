using Microsoft.EntityFrameworkCore;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Repository.Usuarios.Interfaces;
using System.Threading.Tasks;
using Sellius.API.Domain.Entity.Users;
using Sellius.API.Infra.Context;

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

        public async Task<TypeUser> BuscaDireto(TypeUser obj)
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

        public async Task<bool> Create(TypeUser obj)
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

        public async Task<bool> Delete(TypeUser obj)
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

        public Task<IEnumerable<TypeUser>> Filtrar(TypeUser obj)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginacaoTabelaResult<TypeUser, TypeUser>> Filtrar(PaginacaoTabelaResult<TypeUser, TypeUser> obj)
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

        public async Task<List<TypeUser>> recuperaTpUsuarios(int idEmpresa)
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

        public async Task<bool> Update(TypeUser obj)
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

        public async Task<List<TypeUserXMenu>> obterTodosTpUsuarioXMenu(int idTpUsuario)
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

        public async Task RemoverTodosTpUsuariosXMenu(List<TypeUserXMenu> usuarioXMenus)
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

        public async Task AdicionarTodosTpUsuariosXMenu(List<TypeUserXMenu> usuarioXMenus)
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

        public async Task<UserConfiguration> obterConfiguracao(int idTpUsuario)
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

        public async Task RemoverConfiguracao(UserConfiguration usuarioXMenus)
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

        public async Task AdicionarConfiguracao(UserConfiguration usuarioXMenus)
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

        public async Task<List<TypeUser>>obterTodosTpUsuarios(int idEmpresa)
        {
            return await _context.TpUsuarios.Where(c => c.idEmpresa == idEmpresa).ToListAsync();
        }
    }
}
