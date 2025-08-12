using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.Models;

namespace Sellius.API.DTOs.TabelasDTOs
{
    public class PedidoTabela
    {
        public int id { get; set; }
        public int qtd { get; set; }
        public string Cliente { get; set; }
        public string Usuario { get; set; }
        public List<PedidoXProdutoDTO> Produto { get; set; }
        public short Finalizado { get; set; }
        public DateTime dthPedido { get; set; }
        
        public static implicit operator PedidoTabela(PedidoModel model)
        {
            return new PedidoTabela
            {
                id = model.id,
                qtd = model.qtd,
                Cliente = model.Cliente.Nome,
                Usuario = model.Usuario.Nome,
                Finalizado = model.Finalizado,
                dthPedido = model.dthPedido,
                Produto = PedidoXProdutoDTO.FromList(model.Produto)
            };
        }
        public static List<PedidoTabela> FromList(List<PedidoModel> models)
        {
            List<PedidoTabela> ret = new List<PedidoTabela>();
            for (int i = 0; i < models.Count; i++)
            {
                ret.Add(models[i]);
            }
            return ret;
        }

    }
}
