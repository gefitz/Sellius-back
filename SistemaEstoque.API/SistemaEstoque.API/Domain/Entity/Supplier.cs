using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;

namespace Sellius.API.Domain.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string? Complement { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public short  Active { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime AlteredDate { get; set; }
        public Guid EnterpriseId { get; set; }
        public long CityId { get; set; }

        public City City { get; set; }
        public Enterprise? Enterprise { get; set; }
        public List<SupplierXCustomer> SupplierXCustomer { get; set; }

        public static implicit operator  Supplier(FornecedorDTO model)
        {
            return new Supplier
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
        public static implicit operator Supplier (FiltroFornecedor filtro)
        {
            return new Supplier
            {
                Nome = filtro.Nome,
                CNPJ = filtro.CNPJ,
                CidadeId= filtro.CidadeId,
                Cidade = new City { EstadoId = filtro.EstadoId},
                EmpresaId = filtro.EmpresaId,
                fAtivo = filtro.fAtivo,
               
            };
        }
    }
}
