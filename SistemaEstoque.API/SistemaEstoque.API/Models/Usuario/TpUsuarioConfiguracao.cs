using Sellius.API.DTOs.CadastrosDTOs;
using System.ComponentModel.DataAnnotations;

namespace Sellius.API.Models.Usuario
{
    public class TpUsuarioConfiguracao
    {
        [Key]
        public int idTpUsuario { get; set; }

        public bool flPodeCriar { get; set; }
        public bool flPodeExcluir { get; set; }
        public bool flPodeEditar { get; set; }
        public bool flPodeInativar { get; set; }
        public bool flPodeAprovar { get; set; }
        public bool flPodeExportar { get; set; }
        public bool flPodeGerenciarUsuarios { get; set; }

        public TpUsuarioModel? TpUsuario { get; set; }

        public static implicit operator TpUsuarioConfiguracao(TpUsuarioConfiguracaoDTO model)
        {
            if (model == null)
            {
                return new TpUsuarioConfiguracaoDTO();
            }


            return new TpUsuarioConfiguracaoDTO
            {
                idTpUsuario = model.idTpUsuario,
                flPodeAprovar = model.flPodeAprovar,
                flPodeCriar = model.flPodeCriar,
                flPodeEditar = model.flPodeEditar,
                flPodeExcluir = model.flPodeExcluir,
                flPodeExportar = model.flPodeExportar,
                flPodeGerenciarUsuarios = model.flPodeGerenciarUsuarios,
                flPodeInativar = model.flPodeInativar,
            };
        }

    }
}
