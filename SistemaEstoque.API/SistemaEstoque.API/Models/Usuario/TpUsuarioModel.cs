using Sellius.API.DTOs.CadastrosDTOs;
using Sellius.API.DTOs.Filtros;
using Sellius.API.Models.Empresa;

namespace Sellius.API.Models.Usuario
{
    public class TpUsuarioModel
    {
        public int id { get; set; }
        public string tpUsuario { get; set; }
        public int idEmpresa { get; set; }
        public DateTime dtCadastro { get; set; }
        public DateTime dtAlteracao { get; set; }
        public short fAtivo { get; set; }

        public EmpresaModel empresa { get; set; }

        public List<TpUsuarioXMenu> tpUsuarioXMenus { get; set; }

        public TpUsuarioConfiguracao TpUsuarioConfigurcao { get; set; }

        public static implicit operator TpUsuarioModel(TpUsuarioDTO dto)
        {
            return new TpUsuarioModel
            { 
                tpUsuario = dto.tpUsuario,
                id = dto.id,
                dtAlteracao = dto.dtAlteracao, 
                idEmpresa = dto.idEmpresa,
                dtCadastro = dto.dtCadastro,
                fAtivo = dto.fAtivo
            };
        }
        public static implicit operator TpUsuarioModel(TpUsuarioFiltro dto)
        {
            return new TpUsuarioModel
            { 
                tpUsuario = dto.tpUsuario,
                fAtivo = dto.fAtivo,
                idEmpresa = (int)dto.idEmpresa,

            };
        }

    }

}
