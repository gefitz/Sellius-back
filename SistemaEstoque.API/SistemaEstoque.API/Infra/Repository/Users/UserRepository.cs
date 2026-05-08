using Sellius.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Sellius.API.DTOs;
using Sellius.API.Services;
using Sellius.API.Repository.Usuarios.Interfaces;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Infra.Context;
using Sellius.API.Infra.Repository.Users.Interfaces;
using Sellius.API.Models.Usuario;

namespace Sellius.API.Repository.Usuarios
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly AppDbContext _context;
        private readonly LogRepository _log;

        public UsuariosRepository(AppDbContext context, LogRepository log)
        {
            _context = context;
            _log = log;
        }

        public async Task<bool> Create(User user)
        {
            try
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return false;
            }

        }

        public Task<bool> Delete(User obj)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> Filtrar(User obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(User obj)
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
        public async Task<User> BuscaDireto(User obj)
        {
            try
            {
                return await _context.Usuarios
                    .Where(u => u.id == obj.id && u.EmpresaId == obj.EmpresaId)
                    .Include(u => u.TipoUsuario)
                    .Include(c => c.Cidade)
                    .ThenInclude(e => e.Estado)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<User> BuscaDiretoEmail(User user)
        {
            try
            {
                User ret = new User();
                if (user.EmpresaId != 0)
                {

                    ret = await _context.Usuarios
                        .Where(u => u.Email == user.Email && u.EmpresaId == user.EmpresaId)
                        .FirstOrDefaultAsync();
                    if (ret != null)
                    {
                        return ret;
                    }
                }
                else
                {
                    ret = await _context.Usuarios.Where(u => u.Email == user.Email).FirstOrDefaultAsync();
                    if (ret != null)
                    {
                        return ret;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return null;
            }
        }

        public async Task<PaginacaoTabelaResult<User, User>> Filtrar(PaginacaoTabelaResult<User, User> obj)
        {
            try
            {
                var query = _context.Usuarios.AsQueryable();

                if (!string.IsNullOrEmpty(obj.Filtro.Nome))
                    query = query.Where(p => p.Nome.Contains(obj.Filtro.Nome));
                if (!string.IsNullOrEmpty(obj.Filtro.Documento))
                    query = query.Where(p => p.Documento.Contains(obj.Filtro.Documento));
                if (obj.Filtro.fAtivo != -1)
                    query = query.Where(p => p.fAtivo.Equals(obj.Filtro.fAtivo));
                if (obj.Filtro.Cidade.id != -1)
                    query = query.Where(p => p.CidadeId.Equals(obj.Filtro.CidadeId));
                query = query.Where(u => u.EmpresaId == obj.Filtro.EmpresaId);

                obj.TotalRegistros = query.Count();
                obj.TotalPaginas = (int)Math.Ceiling((double)obj.TotalRegistros / obj.TamanhoPagina);

                obj.Dados = await query
                    .Include(t => t.TipoUsuario)
                    .Include(c => c.Cidade)
                    .ThenInclude(e => e.Estado)
                    .OrderBy(p => p.id)
                    .Skip((obj.PaginaAtual) * obj.TotalRegistros)
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
    }
}
