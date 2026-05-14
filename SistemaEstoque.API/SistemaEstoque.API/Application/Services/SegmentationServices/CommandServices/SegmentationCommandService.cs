using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.SegmentationServices.CommandServices.Interfaces;
using Sellius.API.Infra.Repository.Cliente.Interfaces;

namespace Sellius.API.Application.Services.SegmentationServices.CommandServices;

public sealed class SegmentationCommandService(
    ISegmentationRepository repository,
    ISegmentationMapper mapper) : ISegmentationCommandService
{
    public async Task<bool> CreateSegmentation(SegmentationRegister dto, Guid enterpriseId)
    {
        var segmentation = mapper.DtoRegisterToMain(dto, enterpriseId);
        segmentation.CreateDate = DateTime.UtcNow;
        segmentation.AlteredDate = DateTime.UtcNow;
        segmentation.Active = 1;

        return await repository.CreateSegmentationAsync(segmentation);
    }

    public async Task<bool> UpdateSegmentation(SegmentationRegister dto)
    {
        var segmentation = await repository.FindByPredicateAsync(s => s.Id == dto.Id);

        if (segmentation is null)
            return false;

        segmentation.Name = dto.Name;
        segmentation.AlteredDate = DateTime.UtcNow;

        return await repository.UpdateSegmentationAsync(segmentation);
    }

    public async Task<bool> InactiveSegmentation(long segmentationId)
    {
        var segmentation = await repository.FindByPredicateAsync(s => s.Id == segmentationId);

        if (segmentation is null)
            return false;

        segmentation.Active = 0;
        segmentation.AlteredDate = DateTime.UtcNow;

        return await repository.UpdateSegmentationAsync(segmentation);
    }
}
