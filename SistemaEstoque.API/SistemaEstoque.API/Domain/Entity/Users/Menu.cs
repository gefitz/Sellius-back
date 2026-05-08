using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.Models.Usuario;

namespace Sellius.API.Domain.Models.Users
{
    public class Menu
    {
        public long Id { get; set; }
        public string DescMenu { get; set; }
        public string UrlMenu { get; set; }
        public string Icon { get; set; }
        public int? MenuFatherId { get; set; }
        public Guid? EnterpriseId { get; set; }
        public short Active { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? AlteredDate { get; set; }

        public List<TypeUserXMenu>? TpUsuarioXMenus { get; init; }
        
        public static implicit operator Menu(MenuDTO dto)
        {
            return new Menu
            {
                Id = dto.Id,
                DeMenu = dto.DeMenu,
                UrlMenu = dto.UrlMenu,
                Icone = dto.Icone,
                IdMenuPai = dto.IdMenuPai,
                FMenuExclusivo = dto.FMenuExclusivo,
                IdEmpresa = dto.IdEmpresa,
                FAtivo = dto.FAtivo,
                DtCadastro = dto.DtCadastro,
                DtAtualizacao = dto.DtAtualizacao
            };
        }

        public static implicit operator Menu(MenuFiltro dto)
        {
            return new Menu
            {
                DeMenu = dto.deMenu,
                IdEmpresa = dto.idEmpresa,
                FAtivo = dto.fAtivo,
            };
        }


    }
}
