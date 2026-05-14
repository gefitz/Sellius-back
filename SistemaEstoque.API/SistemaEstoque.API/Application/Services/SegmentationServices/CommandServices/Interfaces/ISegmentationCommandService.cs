using Sellius.API.Application.DTOs.RegisterDTOs;

namespace Sellius.API.Application.Services.SegmentationServices.CommandServices.Interfaces;

public interface ISegmentationCommandService
{
    Task<bool> CreateSegmentation(SegmentationRegister dto, Guid enterpriseId);
    Task<bool> UpdateSegmentation(SegmentationRegister dto);
    Task<bool> InactiveSegmentation(long segmentationId);
}
