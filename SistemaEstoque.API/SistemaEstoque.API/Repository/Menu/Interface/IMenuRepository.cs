using Sellius.API.Models.Usuario;
using Sellius.API.Repository.Interfaces;

namespace Sellius.API.Repository.Menu.Interface
{
    public interface IMenuRepository:
        IDbMethods<MenuModel>,
        IPaginacao<MenuModel, MenuModel>
    {
        Task<List<MenuModel>> recuperaMenus(int idEmpresa);
    }
}
