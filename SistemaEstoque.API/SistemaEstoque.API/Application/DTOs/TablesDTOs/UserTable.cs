namespace Sellius.API.Application.DTOs.TablesDTOs
{
    public class UserTable
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string TpUsuario { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public short Active { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? AlteredDate { get; set; }
    }
}
