using Sellius.API.Models.Usuario;
using Sellius.API.Repository.Interfaces;

namespace Sellius.API.Repository.Usuarios.Interfaces
{
    public interface ITpUsuarioRepository:IDbMethods<TpUsuarioModel>,IPaginacao<TpUsuarioModel,TpUsuarioModel>
    {
        Task<List<TpUsuarioModel>> recuperaTpUsuarios(int idEmpresa);
    }
}
