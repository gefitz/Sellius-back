using Sellius.API.DTOs.CadastrosDTOs;

namespace Sellius.API.Models.Usuario
{
    public class TpUsuarioXMenu
    {
        public int idTpUsuario { get; set; }
        public int idMenu { get; set; }
        public TpUsuarioModel tpUsuario { get; set; }
        public MenuModel Menu { get; set; }

        public static List<MenuDTO> fromMenuDTOList(List<TpUsuarioXMenu> tpUsuarioXMenus)
        {
            List<MenuDTO> menus = new List<MenuDTO>();
            if(tpUsuarioXMenus != null)
            {
                foreach (var item in tpUsuarioXMenus)
                {
                    menus.Add(item.Menu);
                }
            }
            return menus;
        }
        public TpUsuarioXMenu Clone()
        {
            return (TpUsuarioXMenu)this.MemberwiseClone();
        }
    }
}
