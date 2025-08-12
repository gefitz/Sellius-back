using Sellius.API.DTOs.CadastrosDTOs;

namespace Sellius.API.Models
{
    public class FornecedoresModel
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public short  fAtivo { get; set; }
        public DateTime dthCadastro { get; set; }
        public DateTime dthAlteracao { get; set; }
        public EmpresaModel Empresa { get; set; }
        public int EmpresaId { get; set; }
        public int CidadeId { get; set; }
        public CidadeModel Cidade { get; set; }
        public string CEP { get; set; }
        public string Rua { get; set; }
        public string? Complemento { get; set; }

        public static implicit operator  FornecedoresModel(FornecedorDTO model)
        {
            return new FornecedoresModel
            {
                id = model.id,
                Nome = model.Nome,
                CNPJ = model.CNPJ,
                Telefone = model.Telefone,
                Email = model.Email,
                fAtivo = model.fAtivo,
                dthAlteracao = (DateTime)model.dthAlteracao,
                dthCadastro = (DateTime)model.dthCadastro,
                EmpresaId = (int)model.EmpresaId,
                CidadeId = (int)model.CidadeId,
                CEP = model.CEP,
                Rua = model.Rua,
                Complemento = model.Complemento,
            };
        }

    }
}
