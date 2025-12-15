using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;

namespace Sellius.API.Models
{
    public class MenuModel
    {
        public int Id { get; set; }
        public string DeMenu { get; set; }
        public string UrlMenu { get; set; }
        public string Icone { get; set; }
        public int? IdMenuPai { get; set; }
        public short? FMenuExclusivo { get; set; }
        public int? IdEmpresa { get; set; }
        public short FAtivo { get; set; }

        public DateTime? DtCadastro { get; set; }
        public DateTime? DtAtualizacao { get; set; }


        public static implicit operator MenuModel(MenuDTO dto)
        {
            return new MenuModel
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

        public static implicit operator MenuModel(MenuFiltro dto)
        {
            return new MenuModel
            {
                DeMenu = dto.deMenu,
                IdEmpresa = dto.idEmpresa,
                FAtivo = dto.fAtivo,
            };
        }


    }
}
