using Sellius.API.Models;
using System.ComponentModel.DataAnnotations;

namespace Sellius.API.DTOs.CadastrosDTOs
{
    public class PedidoDTO
    {
        public int id { get; set; }
        public int qtd { get; set; }
        public int ClienteId { get; set; }
        public int UsuarioId { get; set; }
        public short Finalizado { get; set; }
        public List<PedidoXProdutoDTO> Produtos { get; set; }
        public DateTime dthPedido { get; set; }

        public static implicit operator PedidoDTO(PedidoModel model)
        {
            return new PedidoDTO
            {
                id = model.id,
                qtd = model.qtd,
                ClienteId = model.ClienteId,
                UsuarioId = model.UsuarioId,
                Finalizado = model.Finalizado,
                dthPedido = model.dthPedido,
            };
        }
    }
}
