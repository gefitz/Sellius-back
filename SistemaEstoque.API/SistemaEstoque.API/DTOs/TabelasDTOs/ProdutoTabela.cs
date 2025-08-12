using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.Models;

namespace Sellius.API.DTOs.TabelasDTOs
{
    public class ProdutoTabela
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string TipoProduto { get; set; }
        public int TipoProdutoId { get; set; }
        public decimal valor { get; set; }
        public int qtd { get; set; }
        public DateTime dthCriacao { get; set; }
        public DateTime dthAlteracao { get; set; }
        public int fAtivo { get; set; }
        public string Fornecedor { get; set; }
        public int FornecedorId { get; set; }


        public static implicit operator ProdutoTabela(ProdutoModel model)
        {
            return new ProdutoTabela
            {
                id = model.id,
                Nome = model.Nome,
                Descricao = model.Descricao,
                qtd = model.qtd,
                dthCriacao = model.dthCriacao,
                dthAlteracao = model.dthAlteracao,
                fAtivo = model.fAtivo,
                TipoProduto = model.tipoProduto.Tipo,
                Fornecedor = model.Fornecedor.Nome,
                valor = model.valor,
                FornecedorId = model.FornecedorId,
                TipoProdutoId = model.TipoProdutoId

            };
        }
        public static List<ProdutoTabela> FromList(List<ProdutoModel> mode)
        {
            List<ProdutoTabela> ret = new List<ProdutoTabela>();

            for (int i = 0; i < mode.Count; i++)
            {
                ret.Add(mode[i]);
            }
            return ret;
        }
    }
}
