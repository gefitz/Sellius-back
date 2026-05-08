using System.ComponentModel.DataAnnotations;
using Sellius.API.DTOs.CadastrosDTOs;

namespace Sellius.API.Domain.Models.Users
{
    public class UserConfiguration
    {
        [Key]
        public int TpUserId { get; set; }

        public bool flPodeCriar { get; set; }
        public bool flPodeExcluir { get; set; }
        public bool flPodeEditar { get; set; }
        public bool flPodeInativar { get; set; }
        public bool flPodeAprovar { get; set; }
        public bool flPodeExportar { get; set; }
        public bool flPodeGerenciarUsuarios { get; set; }

        public TypeUser? TpUsuario { get; set; }

        public static implicit operator UserConfiguration(TpUsuarioConfiguracaoDTO model)
        {
            if (model == null)
            {
                return new UserConfiguration();
            }


            return new UserConfiguration
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
