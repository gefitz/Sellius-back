using Sellius.API.Models;
using System.ComponentModel.DataAnnotations;

namespace Sellius.API.DTOs.CadastrosDTOs
{
    public class ProdutoDTO
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Necessario o Nome do Produto")]
        [Display(Name = "Nome do Produto")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Necessario a Descrição do Produto")]
        [Display(Name = "Descrição do Produto")]
        public string Descricao { get; set;}

        [Required(ErrorMessage = "Necessario a Tipo do Produto")]
        [Display(Name = "Tipo do Produto")]
        public int? tipoProdutoId { get; set; }

        [Required(ErrorMessage = "Necessario a Valor do Produto")]
        [Display(Name = "Valor do Produto")]
        public decimal valor { get; set; }

        [Required(ErrorMessage = "Necessario a Quantidade do Produto")]
        [Display(Name = "Quantidade do Produto")]
        public int qtd { get; set; }

        public DateTime dthCriacao { get; set; } = DateTime.UtcNow;
        public DateTime dthAlteracao { get; set; } = DateTime.UtcNow;
        public int fAtivo { get; set; }
        public int? FornecedorId { get; set; }
        public int? EmpresaId { get; set; }



        #region Mapper

        public static implicit operator ProdutoDTO(ProdutoModel model)
        {
            return new ProdutoDTO
            {
                id = model.id,
                Nome = model.Nome,
                Descricao = model.Descricao,
                tipoProdutoId = model.TipoProdutoId,
                valor = model.valor,
                qtd = model.qtd,
                dthCriacao = model.dthCriacao,
                dthAlteracao = model.dthAlteracao,
                FornecedorId = model.FornecedorId,
                EmpresaId = model.EmpresaId,
                fAtivo = model.fAtivo
            };
        }

        public static List<ProdutoDTO> FromModelList(List<ProdutoModel> produtosModel)
        {
            List<ProdutoDTO> dtoList = new List<ProdutoDTO>();
            foreach (var item in produtosModel)
            {
                dtoList.Add(item);
            }
            return dtoList;
        }
        #endregion

    }


}
