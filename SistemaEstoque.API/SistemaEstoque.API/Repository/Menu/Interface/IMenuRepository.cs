using Sellius.API.Models;
using Sellius.API.Repository.Interfaces;

namespace Sellius.API.Repository.Menu.Interface
{
    public interface IMenuRepository:
        IDbMethods<MenuModel>,
        IPaginacao<MenuModel, MenuModel>
    {
    }
}
