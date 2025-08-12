using Sellius.API.DTOs.CadastrosDTOs.ClientesCadastros;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sellius.API.Models.Cliente
{
    [Table("Cliente_Grupos")]
    public class GrupoClienteModel
    {
        public int id { get; set; }
        [Column("Grupo")]
        public string nome { get; set; }
        public int idEmpresa { get; set; }
        public EmpresaModel Empresa { get; set; }
        public DateTime dthCriacao { get; set; }
        public DateTime dthAlteracao { get; set; }
        public short fAtivo { get; set; }
        public static implicit operator GrupoClienteModel(GrupoClienteDTO dto)
        {
            return new GrupoClienteModel
            {
                id = dto.id,
                nome = dto.nome,
                idEmpresa = dto.idEmpresa,
                fAtivo = dto.fAtivo,
                dthAlteracao = dto.dthAlteracao,
                dthCriacao = dto.dthCriacao,
            };
        }
    }
}
