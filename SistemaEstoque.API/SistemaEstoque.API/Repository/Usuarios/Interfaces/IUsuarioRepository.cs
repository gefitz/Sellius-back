using Sellius.API.Models.Usuario;
using Sellius.API.Repository.Interfaces;

namespace Sellius.API.Repository.Usuarios.Interfaces
{
    public interface IUsuariosRepository :
        IDbMethods<UsuarioModel>,
        IPaginacao<UsuarioModel,UsuarioModel>
    {
    }
}
