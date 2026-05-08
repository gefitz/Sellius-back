using Sellius.API.Models.Cliente;
using System.ComponentModel.DataAnnotations;

namespace Sellius.API.DTOs.CadastrosDTOs.ClientesCadastros
{
    public class ClienteDTO
    {
        public int id { get; set; }
        [Required(ErrorMessage ="Nome e obrigatorio")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Nome e obrigatorio")]
        [RegularExpression(@"\d+$")]
        public string Documento { get; set; }

        public int  CidadeId { get; set; }

        [Required(ErrorMessage = "Rua e obrigatorio")]
        public string Rua { get; set; }

        [Required(ErrorMessage = "Bairro e obrigatorio")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "CEP e obrigatorio")]
        public string CEP { get; set; }

        public DateTime dthCadastro { get; set; } =  DateTime.UtcNow;
        public DateTime dthAlteracao { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Email e obrigatorio")]
        [EmailAddress(ErrorMessage ="E-mail invalido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefone e obrigatorio")]
        [DisplayFormat(DataFormatString = "{0:(##)#####-####}", ApplyFormatInEditMode = true)]
        public string Telefone { get; set; }
        public int EmpresaId { get; set; }
        public short fAtivo { get; set; }
        public int? idSegmentacao { get; set; }
        public int? idGrupo { get; set; }
        public CidadeDTO? Cidade { get; set; }

        public static implicit operator  ClienteDTO(ClienteModel dto)
        {
            return new ClienteDTO
            {
                id = dto.id,
                Bairro = dto.Bairro,
                CEP = dto.Cep,
                Telefone = dto.Telefone,
                Email = dto.Email,
                CidadeId = dto.CidadeId,
                Documento = dto.Documento,
                dthCadastro = dto.dthCadastro,
                EmpresaId = dto.EmpresaId,
                fAtivo = dto.fAtivo,
                Nome = dto.Nome,
                //Pedidos = dto.Pedidos,
                Rua = dto.Rua,
                idGrupo = dto.idGrupo,
                idSegmentacao = dto.idSegmentacao,
                dthAlteracao = dto.dthAlteracao,
                Cidade = dto.Cidade != null ? dto.Cidade : null
            };
        }

    }
}
