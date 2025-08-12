using Sellius.API.DTOs;
using Sellius.API.DTOs.CadastrosDTOs.ClientesCadastros;
using Sellius.API.DTOs.TabelasDTOs;
using Sellius.API.Models.Cliente;
using Sellius.API.Repository.Cliente.Interfaces;

namespace Sellius.API.Services.segmentacaos
{
    public class SegmentacaoService
    {
        private readonly ISegmentacaoRepository _repository;

        public SegmentacaoService(ISegmentacaoRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<SegmentacaoDTO>> CriarSegmento(SegmentacaoDTO segmentacaoDTO)
        {
            try
            {
                if (await _repository.Create(segmentacaoDTO))
                    return Response<SegmentacaoDTO>.Ok(segmentacaoDTO);
                return Response<SegmentacaoDTO>.Failed("Falha ao tentar cadastrar nova segmentacao");
            }
            catch (Exception ex)
            {
                return Response<SegmentacaoDTO>.Failed(ex.Message);
            }
        }
        public async Task<Response<PaginacaoTabelaResult<SegmentacaoDTO, SegmentacaoDTO>>> Buscarsegmentacaos(PaginacaoTabelaResult<SegmentacaoDTO, SegmentacaoDTO> segmentacaoDTO)
        {
            PaginacaoTabelaResult<SegmentacaoModel, SegmentacaoModel> model = new PaginacaoTabelaResult<SegmentacaoModel, SegmentacaoModel>
            {
                PaginaAtual = segmentacaoDTO.PaginaAtual,
                TamanhoPagina = segmentacaoDTO.TamanhoPagina,
                Filtro = segmentacaoDTO.Filtro,
                TotalPaginas = segmentacaoDTO.TotalPaginas,
                TotalRegistros = segmentacaoDTO.TotalRegistros,

            };
            model = await _repository.Filtrar(model);
            segmentacaoDTO = PaginacaoTabelaResult<SegmentacaoDTO,SegmentacaoDTO>.RetPaginacao(model);
            segmentacaoDTO.Dados = SegmentacaoDTO.FromToList(model.Dados);
            return Response<PaginacaoTabelaResult<SegmentacaoDTO, SegmentacaoDTO>>.Ok(segmentacaoDTO);
        }
        public async Task<Response<SegmentacaoDTO>> BuscarId(int id)
        {
            SegmentacaoModel segmentacao = new SegmentacaoModel { id = id };
            segmentacao = await _repository.BuscaDireto(segmentacao);
            if (segmentacao != null)
                return Response<SegmentacaoDTO>.Ok(segmentacao);
            return Response<SegmentacaoDTO>.Failed("segmentacao não localizado");
        }
        public async Task<Response<SegmentacaoDTO>> Updatesegmentacao(SegmentacaoDTO segmentacaoDTO)
        {
            SegmentacaoModel segmentacao = segmentacaoDTO;
            if (await _repository.Update(segmentacao))
                return Response<SegmentacaoDTO>.Ok();
            return Response<SegmentacaoDTO>.Failed("Falha ao fazer update ao segmentacao");
        }
        public async Task<Response<SegmentacaoDTO>> Inativarsegmentacao(int id)
        {
            var segmentacaoInativar = await BuscarId(id);
            if (!segmentacaoInativar.success)
                return segmentacaoInativar;
            SegmentacaoModel segmentacao = segmentacaoInativar.Data;
            segmentacao.fAtivo = 0;
            return await Updatesegmentacao(segmentacao);

        }
    }
}