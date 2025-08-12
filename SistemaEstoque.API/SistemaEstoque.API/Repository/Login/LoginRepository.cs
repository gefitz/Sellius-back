using Microsoft.EntityFrameworkCore;
using Sellius.API.Context;
using Sellius.API.Models;
using Sellius.API.Repository.Interfaces;
using Sellius.API.Repository.Login.Interfaces;

namespace Sellius.API.Repository.Login
{
    public class LoginRepository : ILoginRepository
    {
        private AppDbContext _context;
        private LogRepository _log;

        public LoginRepository(AppDbContext context, LogRepository log)
        {
            _context = context;
            _log = log;
        }

        public async Task<LoginModel> BuscaDireto(LoginModel obj)
        {
            try
            {
                return await _context.Logins.Include(u => u.Usuario).Where(l => l.Email == obj.Email).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return null;
            }
        }

        public async Task<bool> Create(LoginModel obj)
        {
            try
            {
                _context.Logins.Add(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return false;
            }
        }

        public Task<bool> Delete(LoginModel obj)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LoginModel>> Filtrar(LoginModel obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(LoginModel obj)
        {
            try
            {
                _context.Logins.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return false;
            }
        }
        public async Task<bool> VereficaEmailExistente(LoginModel obj)
        {
            try
            {
                var login = _context.Logins.Where(l => l.Email == obj.Email && l.EmpresaId == obj.EmpresaId).FirstOrDefault();
                if (login != null)
                {
                    return true;
                }
                return false;
            }catch (Exception ex)
            {
                _log.Error(ex);
                throw;
            }
        }
    }
}
