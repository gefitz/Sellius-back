using Sellius.API.Models;
using Sellius.API.Repository.Interfaces;

namespace Sellius.API.Repository.Login.Interfaces
{
    public interface ILoginRepository : IDbMethods<LoginModel>
    {
        public Task<bool> VereficaEmailExistente(LoginModel model);
    }
}
