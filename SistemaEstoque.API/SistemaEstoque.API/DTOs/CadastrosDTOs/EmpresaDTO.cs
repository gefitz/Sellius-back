using Sellius.API.Enums;
using Sellius.API.Models;

namespace Sellius.API.DTOs.CadastrosDTOs
{
    public class EmpresaDTO
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int CidadeId { get; set; }
        public string CEP { get; set; }
        public string Rua { get; set; }
        public DateTime dthCadastro { get; set; } = DateTime.UtcNow;
        public DateTime dthAlteracao { get; set; } = DateTime.UtcNow;
        public TipoLicenca? TipoLicenca { get; set; }
        public short fAtivo { get; set; } = 0;

        public static implicit operator  EmpresaDTO(EmpresaModel dto)
        {
            return new EmpresaDTO
            {
                id = dto.id,
                Nome = dto.Nome,
                CNPJ = dto.CNPJ,
                Telefone = dto.Telefone,
                Email = dto.Email,
                Rua = dto.Rua,
                CidadeId = dto.CidadeId,
                CEP = dto.CEP,
                dthAlteracao = (DateTime)dto.dthAlteracao,
                dthCadastro = (DateTime)dto.dthCadastro,
                fAtivo = dto.fAtivo,
            };
        }
        public static List<EmpresaDTO> FromList(List<EmpresaModel> list)
        {
            List<EmpresaDTO> dTOs = new List<EmpresaDTO>();
            for (int i = 0; i < list.Count; i++)
            {
                dTOs.Add(list[i]);
            }
            return dTOs;
        }
    }
}
