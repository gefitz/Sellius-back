using Sellius.API.Models;

namespace Sellius.API.DTOs.TabelasDTOs
{
    public class FornecedorTabelaResult
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public short fAtivo { get; set; }
        public DateTime dthCadastro { get; set; } = DateTime.UtcNow;
        public DateTime dthAlteracao { get; set; } = DateTime.UtcNow;
        public string Cidade { get; set; }

        public static implicit operator FornecedorTabelaResult(FornecedoresModel model)
        {
            return new FornecedorTabelaResult
            {
                id = model.id,
                Nome = model.Nome,
                CNPJ = model.CNPJ,
                Telefone = model.Telefone,
                Email = model.Email,
                fAtivo = model.fAtivo,
                dthAlteracao = model.dthAlteracao,
                dthCadastro = model.dthCadastro,
                Cidade = model.Cidade.Cidade + " / " + model.Cidade.Estado.Estado

            };
        }
        public static List<FornecedorTabelaResult> FromList(List<FornecedoresModel> list)
        {
            List<FornecedorTabelaResult> fornecedors = new List<FornecedorTabelaResult>();
            for (int i = 0; i < list.Count; i++)
            {
                fornecedors.Add(list[i]);
            };
            return fornecedors;
        } 

    }
}
