namespace Sellius.API.DTOs.Filtros
{
    public class UsuarioFiltro
    {
        public string? Nome { get; set; }
        public int TpUsuario { get; set; } = -1;
        public string? Cpf { get; set; }
        public int Cidade { get; set; }= -1;
        public int Estado { get; set; } = -1;
        public short FAtivo { get; set; } = -1;
        public int idEmpresa { get; set; }
    }   
}
