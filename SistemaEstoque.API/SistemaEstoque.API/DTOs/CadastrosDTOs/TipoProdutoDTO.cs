using Sellius.API.Models;
using System.ComponentModel.DataAnnotations;

namespace Sellius.API.DTOs.CadastrosDTOs
{
    public class TipoProdutoDTO
    {
        public int id { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public int EmpresaId { get; set; }
        public short fAtivo { get; set; }

        public static implicit operator TipoProdutoDTO(TipoProdutoModel model)
        {
            return new TipoProdutoDTO
            {
                id = model.id,
                Tipo = model.Tipo,
                Descricao = model.Descricao,
                EmpresaId = model.Empresaid,
                fAtivo = model.fAtivo
            };
        }
        public static List<TipoProdutoDTO> FromList(List<TipoProdutoModel> model)
        {
            List<TipoProdutoDTO> dTOs = new List<TipoProdutoDTO>();
            for (int i = 0; i < model.Count; i++)
            {
                dTOs.Add(model[i]);
            }
            return dTOs;
        }
    }
}