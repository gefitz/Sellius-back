using Sellius.API.Models.Empresa;
using Sellius.API.Models.Usuario;
using System.Collections.Generic;

namespace Sellius.API.DTOs.CadastrosDTOs
{
    public class TpUsuarioDTO
    {
        public int id { get; set; }
        public string tpUsuario { get; set; }
        public int idEmpresa { get; set; }
        public DateTime dtCadastro { get; set; }
        public DateTime dtAlteracao { get; set; } = DateTime.UtcNow;
        public short fAtivo { get; set; }
        public List<MenuDTO> Menu { get; set; }
        public TpUsuarioConfiguracaoDTO? TpUsuarioConfiguracao { get; set; }

        public static implicit operator TpUsuarioDTO(TpUsuarioModel dto)
        {
            return new TpUsuarioDTO
            {
                tpUsuario = dto.tpUsuario,
                id = dto.id,
                dtAlteracao = dto.dtAlteracao,
                idEmpresa = dto.idEmpresa,
                dtCadastro = dto.dtCadastro,
                fAtivo = dto.fAtivo,
                Menu = TpUsuarioXMenu.fromMenuDTOList(dto.tpUsuarioXMenus),
                TpUsuarioConfiguracao = dto.TpUsuarioConfigurcao
            };
        }

        public static List<TpUsuarioDTO> fromToList(List<TpUsuarioModel> models)
        {
            List<TpUsuarioDTO> dTOs = new List<TpUsuarioDTO>();
            foreach (var model in models)
            {
                dTOs.Add(model);
            }
            return dTOs;

        }

        public TpUsuarioDTO Clone()
        {
            return (TpUsuarioDTO) this.MemberwiseClone();
        }
    }
}
