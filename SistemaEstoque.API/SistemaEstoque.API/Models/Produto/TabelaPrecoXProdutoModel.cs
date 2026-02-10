namespace Sellius.API.Models.Produto
{
    public class TabelaPrecoXProdutoModel
    {
        public int idTabelaPreco { get; set; }
        public int idProduto { get; set; }
        public decimal vlPreco { get; set; }
        public ProdutoModel Produto { get; set; }
        public TabelaPrecoModel TabelaPreco { get; set; }
    }
}
