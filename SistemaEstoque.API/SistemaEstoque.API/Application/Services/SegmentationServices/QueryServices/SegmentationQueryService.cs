using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.SegmentationServices.QueryServices.Interfaces;
using Sellius.API.Infra.Repository.Cliente.Interfaces;

namespace Sellius.API.Application.Services.SegmentationServices.QueryServices;

public sealed class SegmentationQueryService(
    ISegmentationRepository repository,
    ISegmentationMapper mapper) : ISegmentationQueryService
{
    public async Task<SegmentationEdit> FindBySegmentationId(long segmentationId)
    {
        var segmentation = await repository.FindByPredicateAsync(s => s.Id == segmentationId);
        return segmentation is not null ? mapper.MainToDtoEdit(segmentation) : new SegmentationEdit();
    }

    public async Task<List<SegmentationTableReturn>> FindAllSegmentations(SegmentationFilter filter, Guid enterpriseId)
    {
        var segmentations = await repository.FindAllAsync(
            s => s.EnterpriseId == enterpriseId
                 && (filter.Name == null || s.Name.Contains(filter.Name))
                 && (filter.Active < 0 || s.Active == filter.Active),
            null,
            o => o.OrderBy(s => s.Name));

        return mapper.MainToTableList(segmentations);
    }
}
