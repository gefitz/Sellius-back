using Sellius.API.Domain.Models.Produto;
using Sellius.API.DTOs.CadastrosDTOs;

namespace Sellius.API.Domain.Models.SaleOrder
{
    public class SaleOrdeXProduct
    { 
        public long SaleOrderId { get; set; }
        public long ProductId { get; set; }
        public int Qtd { get; set; }
        public decimal PriveSeller { get; set; }

        public Product? Product { get; set; }
        public SaleOrder? SaleOrder { get; set; }
        public static implicit operator SaleOrdeXProduct(PedidoXProdutoDTO model)
        {
            return new SaleOrdeXProduct
            {
                idPedido = model.idPedido,
                idProduto = model.produto.id,
                qtd = model.qtd,
                ValorVenda = model.ValorVenda
            };
        }

        public static List<SaleOrdeXProduct> FromList(List<PedidoXProdutoDTO> list)
        {
            List<SaleOrdeXProduct> ret = new List<SaleOrdeXProduct> ();

            for (int i = 0; i < list.Count; i++)
            {
                ret.Add(list[i]);
            }
            return ret;
        }
    }
}
