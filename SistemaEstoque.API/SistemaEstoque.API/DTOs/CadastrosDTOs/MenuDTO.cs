using Sellius.API.Enums;
using Sellius.API.Models;
using System.ComponentModel.DataAnnotations;

namespace Sellius.API.DTOs.CadastrosDTOs
{
    public class MenuDTO
    {
        public int Id { get; set; }
        public string DeMenu { get; set; }
        public string UrlMenu { get; set; }
        public string Icone { get; set; }
        public int IdMenuPai { get; set; }
        public bool FMenuExclusivo { get; set; }
        public int? IdEmpresa { get; set; }
        public bool FAtivo { get; set; }
        public DateTime DtCadastro { get; set; }
        public DateTime DtAtualizacao { get; set; }

        public static implicit operator MenuDTO(MenuModel model)
        {            
            return new MenuDTO
            {
                Id = model.Id,
                DeMenu = model.DeMenu,
                UrlMenu = model.UrlMenu,
                Icone = model.Icone,
                IdMenuPai = model.IdMenuPai,
                FMenuExclusivo = model.FMenuExclusivo,
                IdEmpresa = model.IdEmpresa,
                FAtivo = model.FAtivo,
                DtCadastro = model.DtCadastro,
                DtAtualizacao = model.DtAtualizacao
            };               
        }

        public static List<MenuDTO> FromList(List<MenuModel> list)
        {
            List<MenuDTO> dTOs = new List<MenuDTO>();
            for (int i = 0; i < list.Count; i++)
            {
                dTOs.Add(list[i]);
            }
            return dTOs;
        }
    }
}
