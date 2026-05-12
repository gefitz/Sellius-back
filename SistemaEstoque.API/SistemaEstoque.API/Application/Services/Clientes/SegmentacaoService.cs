using Sellius.API.Application.DTOs.RegisterDTOs.CustomerRegisterDTOs;
using Sellius.API.Domain.Entity.EntityCustomers;
using Sellius.API.Domain.Models;
using Sellius.API.DTOs;
using Sellius.API.Repository.Cliente;
using Sellius.API.Repository.Cliente.Interfaces;

namespace Sellius.API.Services.segmentacaos
{
    public class SegmentacaoService
    {
        private readonly SegmentacaoRepository _repository;

        public SegmentacaoService(SegmentacaoRepository repository)
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
        public async Task<Response<PaginationTableResult<>>> Buscarsegmentacaos(PaginationTableResult<> segmentacaoDTO)
        {
            PaginationTableResult<> model = new PaginationTableResult<>
            {
                PaginaAtual = segmentacaoDTO.PaginaAtual,
                TamanhoPagina = segmentacaoDTO.TamanhoPagina,
                Filtro = segmentacaoDTO.Filtro,
                TotalPaginas = segmentacaoDTO.TotalPaginas,
                TotalRegistros = segmentacaoDTO.TotalRegistros,

            };
            model = await _repository.Filtrar(model);
            segmentacaoDTO = PaginationTableResult<>.RetPaginacao(model);
            segmentacaoDTO.Dados = SegmentacaoDTO.FromToList(model.Dados);
            return Response<PaginationTableResult<>>.Ok(segmentacaoDTO);
        }
        public async Task<Response<SegmentacaoDTO>> BuscarId(int id)
        {
            Segmentation segmentacao = new Segmentation { id = id };
            segmentacao = await _repository.BuscaDireto(segmentacao);
            if (segmentacao != null)
                return Response<SegmentacaoDTO>.Ok(segmentacao);
            return Response<SegmentacaoDTO>.Failed("segmentacao não localizado");
        }
        public async Task<Response<SegmentacaoDTO>> Updatesegmentacao(SegmentacaoDTO segmentacaoDTO)
        {
            Segmentation segmentacao = segmentacaoDTO;
            if (await _repository.Update(segmentacao))
                return Response<SegmentacaoDTO>.Ok();
            return Response<SegmentacaoDTO>.Failed("Falha ao fazer update ao segmentacao");
        }
        public async Task<Response<SegmentacaoDTO>> Inativarsegmentacao(int id)
        {
            var segmentacaoInativar = await BuscarId(id);
            if (!segmentacaoInativar.success)
                return segmentacaoInativar;
            Segmentation segmentacao = segmentacaoInativar.Data;
            segmentacao.fAtivo = 0;
            return await Updatesegmentacao(segmentacao);

        }
        public async Task<Response<List<SegmentacaoDTO>>> CarregarCombo(int idEmpresa)
        {
            var segmentacoes = SegmentacaoDTO.FromToList(await _repository.CarregarCombo(idEmpresa));
            return Response<List<SegmentacaoDTO>>.Ok(segmentacoes);
        }
    }
}