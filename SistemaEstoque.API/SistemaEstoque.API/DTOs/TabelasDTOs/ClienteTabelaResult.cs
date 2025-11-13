using Sellius.API.DTOs.CadastrosDTOs.ClientesCadastros;
using Sellius.API.Models.Cliente;
using System.ComponentModel.DataAnnotations;

namespace Sellius.API.DTOs.TabelasDTOs
{
    public class ClienteTabelaResult
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string CidadeEstado { get; set; }
        public string Rua { get; set; }
        public DateTime dthCadastro { get; set; }
        public DateTime dthAlteracao { get; set; }
        public short fAtivo { get; set; }
        public string Grupo { get; set; }
        public string Segmentacao { get; set; }
        public static implicit operator ClienteTabelaResult(ClienteModel model)
        {
            return new ClienteTabelaResult
            {
                id = model.id,
                Nome = model.Nome,
                Documento = model.Documento,
                Rua = model.Rua,
                Telefone = model.Telefone,
                fAtivo = model.fAtivo,
                CidadeEstado = model.Cidade.Cidade + " / " + model.Cidade.Estado.Sigla,
                Grupo = model.Grupo != null ? model.Grupo.nome : "",
                Segmentacao = model.segmentacao != null ? model.segmentacao.Segmento : "",
                dthCadastro = model.dthCadastro,
                dthAlteracao = model.dthAlteracao
            };
        }
        public static List<ClienteTabelaResult> FromToList(List<ClienteModel> list)
        {
            List<ClienteTabelaResult> dto = new List<ClienteTabelaResult>();
            for (int i = 0; i < list.Count; i++)
            {
                dto.Add(list[i]);
            }
            return dto;
        }
    }
}
