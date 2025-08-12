using Sellius.API.Models.Cliente;
using Sellius.API.Repository.Interfaces;
using System.Data;

namespace Sellius.API.Repository.Cliente.Interfaces
{
    public interface ISegmentacaoRepository : IDbMethods<SegmentacaoModel>, IPaginacao<SegmentacaoModel,SegmentacaoModel>
    {
        Task<List<SegmentacaoModel>>CarregarCombo(int idEmpresa);
    }
}
