using Sellius.API.Models.Usuario;

namespace Sellius.API.DTOs.TabelasDTOs
{
    public class UsuarioTabela
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string TpUsuario { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public short fAtivo { get; set; }

        public DateTime DtCadastro { get; set; }
        public DateTime? DtAlteracao { get; set; }

        public static implicit operator UsuarioTabela(UsuarioModel model)
        {
            return new UsuarioTabela
            {
                Id = model.id,
                Nome = model.Nome,
                TpUsuario = model.TipoUsuario.tpUsuario,
                Cidade = model.Cidade.Cidade + " / " + model.Cidade.Estado.Sigla,
                Email = model.Email,
                Endereco = model.Rua,
                fAtivo = model.fAtivo
            };
        }
        public static List<UsuarioTabela> FromList(List<UsuarioModel> model)
        {
            List<UsuarioTabela> tabela = new List<UsuarioTabela>();

            for (int i = 0; i < model.Count; i++)
            {
                tabela.Add(model[i]);
            }
            return tabela;
        }
    }
}
