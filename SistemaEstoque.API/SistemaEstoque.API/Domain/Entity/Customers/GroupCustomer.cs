using System.ComponentModel.DataAnnotations.Schema;
using Sellius.API.DTOs.CadastrosDTOs.ClientesCadastros;

namespace Sellius.API.Domain.Models.Customer
{
    [Table("Cliente_Grupos")]
    public class GroupCustomer
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime AlteredDate { get; set; }
        public short Active { get; set; }
        public Guid EnterpriseId { get; set; }
        
        public Entity.Enterprise.Enterprise Enterprise { get; set; }
        public static implicit operator GroupCustomer(GrupoClienteDTO dto)
        {
            return new GroupCustomer
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
