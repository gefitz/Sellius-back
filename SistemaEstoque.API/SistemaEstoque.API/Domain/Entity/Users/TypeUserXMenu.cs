using Sellius.API.DTOs.CadastrosDTOs;

namespace Sellius.API.Domain.Models.Users
{
    public class TypeUserXMenu
    {
        public int idTpUsuario { get; set; }
        public int idMenu { get; set; }
        public TypeUser tpUsuario { get; set; }
        public Menu Menu { get; set; }

        public static List<MenuDTO> fromMenuDTOList(List<TypeUserXMenu> tpUsuarioXMenus)
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
        public TypeUserXMenu Clone()
        {
            return (TypeUserXMenu)this.MemberwiseClone();
        }
    }
}
