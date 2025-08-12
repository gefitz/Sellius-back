using Sellius.API.Models;
using Sellius.API.Repository.Interfaces;

namespace Sellius.API.Repository.Pedidos.Interfaces
{
    public interface IPedidoRepository:
        IDbMethods<PedidoModel>,
        IPaginacao<PedidoModel,PedidoModel>
        
    {
    }
}
