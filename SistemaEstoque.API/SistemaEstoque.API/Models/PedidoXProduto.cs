using Sellius.API.DTOs.CadastrosDTOs;

namespace Sellius.API.Models
{
    public class PedidoXProduto
    {
        public int id { get; set; }

        public int idPedido { get; set; }
        public PedidoModel Pedido { get; set; }
        public int idProduto { get; set; }
        public ProdutoModel Produto { get; set; }
        public int qtd { get; set; }
        public float ValorVenda { get; set; }

        public static implicit operator PedidoXProduto(PedidoXProdutoDTO model)
        {
            return new PedidoXProduto
            {
                idPedido = model.idPedido,
                idProduto = model.idProduto,
                qtd = model.qtd,
                ValorVenda = model.ValorVenda
            };
        }

        public static List<PedidoXProduto> FromList(List<PedidoXProdutoDTO> list)
        {
            List<PedidoXProduto> ret = new List<PedidoXProduto> ();

            for (int i = 0; i < list.Count; i++)
            {
                ret.Add(list[i]);
            }
            return ret;
        }
    }
}
