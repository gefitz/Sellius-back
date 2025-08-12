using Sellius.API.DTOs;
using Sellius.API.Enums;

namespace Sellius.API.Models
{
    public class LicencaModel
    {
        public int id { get; set; }
        public Guid Licenca { get; set; }
        public DateTime dthVencimento { get; set; }
        public DateTime dthInicioLincenca { get; set; }
        public decimal ValorMensal { get; set; }
        public int UsuairosIncluirFree { get; set; }
        public int UsuariosIncluidos { get; set; }
        public decimal ValorPorUsuario { get; set; }
        public TipoLicenca TipoLincenca { get; set; }
    }
}