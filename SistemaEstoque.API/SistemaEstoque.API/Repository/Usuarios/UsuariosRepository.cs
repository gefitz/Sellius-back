using Sellius.API.Context;
using Sellius.API.Models;
using Sellius.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Sellius.API.DTOs;
using Sellius.API.Services;
using Sellius.API.Repository.Usuarios.Interfaces;
using Sellius.API.DTOs.TabelasDTOs;

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

        public async Task<bool> Create(UsuarioModel usuario)
        {
            try
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return false;
            }

        }

        public Task<bool> Delete(UsuarioModel obj)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UsuarioModel>> Filtrar(UsuarioModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(UsuarioModel obj)
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
        public async Task<UsuarioModel> BuscaDireto(UsuarioModel usuario)
        {
            try
            {
                UsuarioModel ret = new UsuarioModel();
                if (usuario.EmpresaId != 0)
                {

                    ret = await _context.Usuarios.Where(u => u.Email == usuario.Email && u.EmpresaId == usuario.EmpresaId).FirstOrDefaultAsync();
                    if (ret != null)
                    {
                        return ret;
                    }
                }
                else
                {
                    ret = await _context.Usuarios.Where(u => u.Email == usuario.Email).FirstOrDefaultAsync();
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

        public async Task<PaginacaoTabelaResult<UsuarioModel, UsuarioModel>> Filtrar(PaginacaoTabelaResult<UsuarioModel, UsuarioModel> obj)
        {
            try
            {
                var query = _context.Usuarios.AsQueryable();

                if (!string.IsNullOrEmpty(obj.Filtro.Nome))
                    query = query.Where(p => p.Nome.Contains(obj.Filtro.Nome));
                if (!string.IsNullOrEmpty(obj.Filtro.Documento))
                    query = query.Where(p => p.Documento.Contains(obj.Filtro.Documento));
                if (obj.Filtro.fAtivo != 0)
                    query = query.Where(p => p.fAtivo.Equals(obj.Filtro.fAtivo));
                if (obj.Filtro.CidadeId != 0)
                    query = query.Where(p => p.CidadeId.Equals(obj.Filtro.CidadeId));
                query = query.Where(u => u.EmpresaId == obj.Filtro.EmpresaId);

                obj.TotalRegistros = query.Count();
                obj.TotalPaginas = (int)Math.Ceiling((double)obj.TotalRegistros / obj.TamanhoPagina);

                obj.Dados = await query
                    .OrderBy(p => p.id)
                    .Skip((obj.PaginaAtual - 1) * obj.TotalRegistros)
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
