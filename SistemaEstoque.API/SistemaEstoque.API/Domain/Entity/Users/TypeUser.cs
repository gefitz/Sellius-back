using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.Models.Usuario;

namespace Sellius.API.Domain.Models.Users
{
    public class TypeUser
    {
        public long Id { get; init; }
        public string NameType { get; set; }
        public Guid EnterpriseId { get; init; }
        public DateTime CreateDate { get; init; }
        public DateTime AlteredDate { get; set; }
        public short Active { get; set; }

        public Enterprise? Enterprise { get; init; }

        public List<TypeUserXMenu>? TpUsuarioXMenus { get; init; }

        public UserConfiguration? TpUsuarioConfigurcao { get; init; }

        public static implicit operator TypeUser(TpUsuarioDTO dto)
        {
            return new TypeUser
            { 
                tpUsuario = dto.tpUsuario,
                id = dto.id,
                dtAlteracao = dto.dtAlteracao, 
                idEmpresa = dto.idEmpresa,
                dtCadastro = dto.dtCadastro,
                fAtivo = dto.fAtivo
            };
        }
        public static implicit operator TypeUser(TpUsuarioFiltro dto)
        {
            return new TypeUser
            { 
                tpUsuario = dto.tpUsuario,
                fAtivo = dto.fAtivo,
                idEmpresa = (int)dto.idEmpresa,

            };
        }

    }

}
