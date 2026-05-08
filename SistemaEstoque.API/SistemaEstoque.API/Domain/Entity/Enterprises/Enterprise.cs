using Sellius.API.Domain.Models;
using Sellius.API.Domain.Models.Enterprise;
using Sellius.API.DTOs.CadastrosDTOs;

namespace Sellius.API.Domain.Entity
{
    public class Enterprise
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int CidadeId { get; set; }
        public City Cidade { get; set; }
        public string CEP { get; set; }
        public string Rua { get; set; }
        public int LicencaId { get; set; }
        public LicencaModel Licenca { get; set; }
        public DateTime dthCadastro { get; set; }
        public DateTime dthAlteracao { get; set; }
        public short fAtivo { get; set; }

        public static implicit operator Enterprise(EmpresaDTO dto)
        {
            return new Enterprise
            {
                id = dto.id,
                Nome = dto.Nome,
                CNPJ = dto.CNPJ,
                Telefone = dto.Telefone,
                Email = dto.Email,
                Rua = dto.Rua,
                CidadeId = dto.CidadeId,
                CEP = dto.CEP,
                dthAlteracao = dto.dthAlteracao,
                dthCadastro = dto.dthCadastro,
                fAtivo = dto.fAtivo
            };
        }
    }
}
