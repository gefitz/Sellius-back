using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.DTOs.TabelasDTOs;
using System.ComponentModel.DataAnnotations;

namespace Sellius.API.Models
{
    public class ProdutoModel
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int TipoProdutoId {  get; set; }
        public TipoProdutoModel tipoProduto { get; set; }
        public decimal valor { get; set; }
        public int qtd { get; set; }
        public DateTime dthCriacao { get; set; }
        public DateTime dthAlteracao { get; set; }
        public IEnumerable<PedidoXProduto> pedidos { get; set; }
        public int fAtivo { get; set; }
        public int FornecedorId { get; set; }
        public FornecedoresModel Fornecedor { get; set; }
        public EmpresaModel Empresa { get; set; }
        public int EmpresaId { get; set; }


        #region Mapper
        public static implicit operator ProdutoModel(FiltroProduto filtroProduto)
        {
            return new ProdutoModel
            {
                Nome = filtroProduto.Nome,
                tipoProduto = new TipoProdutoModel { id = filtroProduto.tipoProdutoId },

            };
        }
        public static implicit operator ProdutoModel(ProdutoDTO produtoDTO)
        {
            return new ProdutoModel
            {
                id = produtoDTO.id,
                Nome = produtoDTO.Nome,
                Descricao = produtoDTO.Descricao,
                TipoProdutoId = (int)produtoDTO.tipoProdutoId,
                valor = produtoDTO.valor,
                qtd = produtoDTO.qtd,
                dthCriacao = produtoDTO.dthCriacao,
                dthAlteracao = produtoDTO.dthAlteracao,
                FornecedorId = (int)produtoDTO.FornecedorId,
                EmpresaId = (int)produtoDTO.EmpresaId,
                fAtivo = produtoDTO.fAtivo
            };
        }
        public static implicit operator ProdutoModel(ProdutoTabela model)
        {
            return new ProdutoModel
            {
                id = model.id
            };
        }
        #endregion
    }
}
