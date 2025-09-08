using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Models;

namespace Sellius.API.DTOs.CadastrosDTOs
{
    public class PedidoXProdutoDTO
    {
        public int idPedido { get; set; }
        public ProdutoTabela produto { get; set; }
        public int qtd { get; set; }
        public float ValorVenda { get; set; }
        public int id { get; set; }

        public static implicit operator PedidoXProdutoDTO (PedidoXProduto model)
        {
            return new PedidoXProdutoDTO
            {
                idPedido = model.idPedido,
                produto = model.Produto,
                qtd = model.qtd,
                ValorVenda = model.ValorVenda,
                id = model.id
            };
        }
        public static List<PedidoXProdutoDTO> FromList(List<PedidoXProduto> models)
        {
            List<PedidoXProdutoDTO> ret = new List<PedidoXProdutoDTO>();
            for (int i = 0; i < models.Count; i++)
            {
                ret.Add(models[i]);
            }
            return ret;
        }

    }
}
