using Sellius.API.DTOs.CadastrosDTOs;

namespace Sellius.API.Models
{
    public class TipoProdutoModel
    {
        public int id { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public short fAtivo { get; set; }
        public EmpresaModel Empresa { get; set; }
        public int Empresaid { get; set; }
        public List<ProdutoModel> Produtos { get; set; }


        public static implicit operator TipoProdutoModel(TipoProdutoDTO dto)
        {
            return new TipoProdutoModel
            {
                Tipo = dto.Tipo,
                Descricao = dto.Descricao,
                Empresaid = dto.EmpresaId,
                fAtivo = dto.fAtivo,
                id = dto.id
            };
        }
    }
}