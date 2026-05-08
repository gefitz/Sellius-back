using Sellius.API.Domain.Entity.Users;
using Sellius.API.Repository.Interfaces;

namespace Sellius.API.Repository.Usuarios.Interfaces
{
    public interface ITpUsuarioRepository:IDbMethods<TypeUser>,IPaginacao<TypeUser,TypeUser>
    {
        Task<List<TypeUser>> recuperaTpUsuarios(int idEmpresa);
    }
}
