using System.ComponentModel.DataAnnotations;
using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.Models;

namespace Sellius.API.Domain.Models.Users
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
        public long TpUsuarioId { get; set; }
        public short Active { get; set; }
        public Guid EnterpriseId { get; set; }
        public long CityId { get; set; }

        public Enterprise Enterprise { get; set; }
        public TypeUser TypeUser { get; set; }
        public List<SaleOrder> SaleOrders { get; set; }
        public City City { get; set; } = new City();
        public static implicit operator User(UsuarioDTO dto)
        {
            return new User
            {
                id = dto.id,
                Nome = dto.Nome,
                Documento = dto.Documento,
                Email = dto.Email,
                Rua = dto.Rua,
                CidadeId = dto.CidadeId,
                CEP = dto.CEP,
                dthCadastro = dto.dthCadastro,
                EmpresaId = dto.EmpresaId,
                IdTpUsuario = dto.tipoUsuario,
                fAtivo = dto.fAtivo,
            };
        }
        public static implicit operator User(UsuarioFiltro filtro) => new User
        {
            Nome = filtro.Nome,
            Cidade = new City() { id = filtro.Cidade,EstadoId = filtro.Estado},
            IdTpUsuario = filtro.TpUsuario,
            Documento = filtro.Cpf,
            fAtivo = filtro.FAtivo,
            EmpresaId = filtro.idEmpresa,

        };
    }
}
