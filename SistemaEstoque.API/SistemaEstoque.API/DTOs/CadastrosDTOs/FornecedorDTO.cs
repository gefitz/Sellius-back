using Sellius.API.Models;

namespace Sellius.API.DTOs.CadastrosDTOs
{
    public class FornecedorDTO
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public short fAtivo { get; set; }
        public DateTime dthCadastro { get; set; } = DateTime.UtcNow;
        public DateTime dthAlteracao { get; set; } = DateTime.UtcNow;
        public int EmpresaId { get; set; }
        public int CidadeId { get; set; }
        public string CEP { get; set; }
        public string Rua { get; set; }
        public string? Complemento { get; set; }
        public CidadeDTO? cidade { get; set; }

        public static implicit operator FornecedorDTO(FornecedoresModel model)
        {
            return new FornecedorDTO
            {
                id = model.id,
                Nome = model.Nome,
                CNPJ = model.CNPJ,
                Telefone = model.Telefone,
                Email = model.Email,
                fAtivo = model.fAtivo,
                dthAlteracao = model.dthAlteracao,
                dthCadastro = model.dthCadastro,
                EmpresaId = model.EmpresaId,
                CEP = model.CEP,
                Rua = model.Rua,
                Complemento = model.Complemento,
                cidade = model.Cidade != null ? model.Cidade: new CidadeDTO(),
                CidadeId = model.CidadeId
            };
        }
        public static List<FornecedorDTO> FromList(List<FornecedoresModel> list)
        {
            List<FornecedorDTO> dtp = new List<FornecedorDTO>();
            for (int i = 0; i < list.Count; i++)
            {
                dtp.Add(list[i]);
            }
            return dtp;
        }
    }
}
