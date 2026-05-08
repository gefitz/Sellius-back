using Sellius.API.Domain.Models.Users;
using Sellius.API.DTOs.CadastrosDTOs;

namespace Sellius.API.Domain.Models.SaleOrder
{
    public class SaleOrder
    {
        public long Id { get; set; }
        public int Qtd { get; set; }
        public long CustomerId { get; set; }
        public Customer.Customer Customer { get; set; }
        public short Finished { get; set; }
        public DateTime OrderCreateDate { get; set; }
        public Guid UserId { get; set; }
        public Guid EnterpriseId { get; set; }

        public List<SaleOrdeXProduct> Product { get; set; }
        public User? User { get; set; }
        public Enterprise? Enterprise { get; set; }
        public static implicit operator SaleOrder(PedidoDTO dto)
        {
            return new SaleOrder
            {
                id = dto.id,
                qtd = dto.qtd,
                ClienteId = dto.ClienteId,
                UsuarioId = dto.UsuarioId,
                Finalizado = dto.Finalizado,
                dthPedido = (DateTime)dto.dthPedido,
            };
        }

    }
}
