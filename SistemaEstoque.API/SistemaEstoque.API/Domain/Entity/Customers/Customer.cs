using System.ComponentModel.DataAnnotations;
using Sellius.API.DTOs.CadastrosDTOs.ClientesCadastros;

namespace Sellius.API.Domain.Models.Customer
{
    public class Customer
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public long CityId { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public short Active { get; set; }
        public long? SegmentationId { get; set; }
        public Guid EnterpriseId { get; set; }
        public int? GroupId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime AlteredDate { get; set; }
        
        public City City { get; set; }
        public Segmentation? Segmentation { get; set; }
        public Entity.Enterprise.Enterprise? Enterprise { get; set; }
        public GroupCustomer? Gruop { get; set; }
        public List<SupplierXCustomer>? SupplierXCustomer { get; set; }
        public List<SaleOrder.SaleOrder>? SaleOrders { get; set; }

        public static implicit operator Customer(ClienteDTO dto)
        {
            return new Customer
            {
                id = dto.id,
                Bairro = dto.Bairro,
                Cep = dto.CEP,
                Telefone = dto.Telefone,
                Email = dto.Email,
                CidadeId = dto.CidadeId,
                Documento = dto.Documento,
                dthCadastro = dto.dthCadastro,
                EmpresaId = dto.EmpresaId,
                fAtivo = dto.fAtivo,
                Nome = dto.Nome,
                Rua = dto.Rua,
                idSegmentacao = dto.idSegmentacao == 0 ? null : dto.idSegmentacao,
                idGrupo = dto.idGrupo ==  0 ? null : dto.idGrupo,
                dthAlteracao = dto.dthAlteracao
            }; 
        }
    }
}
