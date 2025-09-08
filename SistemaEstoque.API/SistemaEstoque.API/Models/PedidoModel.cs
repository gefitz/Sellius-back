using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.Models.Cliente;
using System.ComponentModel.DataAnnotations;

namespace Sellius.API.Models
{
    public class PedidoModel
    {
        public int id { get; set; }
        public int qtd { get; set; }
        public int ClienteId { get; set; }
        public ClienteModel Cliente { get; set; }
        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }

        public List<PedidoXProduto> Produto { get; set; }
        public short Finalizado { get; set; }
        public DateTime dthPedido { get; set; }
        public int EmpresaId { get; set; }

        public static implicit operator PedidoModel(PedidoDTO dto)
        {
            return new PedidoModel
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
