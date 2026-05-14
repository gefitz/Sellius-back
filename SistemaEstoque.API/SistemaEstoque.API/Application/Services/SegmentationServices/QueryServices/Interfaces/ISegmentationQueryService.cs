using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.Filters;
using Sellius.API.Application.DTOs.TablesDTOs;

namespace Sellius.API.Application.Services.SegmentationServices.QueryServices.Interfaces;

public interface ISegmentationQueryService
{
    Task<SegmentationEdit> FindBySegmentationId(long segmentationId);
    Task<List<SegmentationTableReturn>> FindAllSegmentations(SegmentationFilter filter, Guid enterpriseId);
}
