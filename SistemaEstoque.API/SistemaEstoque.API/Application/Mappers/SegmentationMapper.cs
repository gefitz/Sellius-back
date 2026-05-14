using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Domain.Entity.EntityCustomers;

namespace Sellius.API.Application.Mappers;

public sealed class SegmentationMapper : ISegmentationMapper
{
    public Segmentation DtoRegisterToMain(SegmentationRegister dto, Guid enterpriseId) => new()
    {
        Name = dto.Name,
        Active = dto.Active,
        EnterpriseId = enterpriseId,
        CreateDate = DateTime.UtcNow,
        AlteredDate = DateTime.UtcNow
    };

    public SegmentationEdit MainToDtoEdit(Segmentation segmentation) => new()
    {
        Id = segmentation.Id,
        Name = segmentation.Name,
        Active = segmentation.Active,
        EnterpriseId = segmentation.EnterpriseId,
        CreateDate = segmentation.CreateDate,
        AlteredDate = segmentation.AlteredDate
    };

    public List<SegmentationTableReturn> MainToTableList(List<Segmentation> segmentations) =>
        segmentations.Select(s => new SegmentationTableReturn
        {
            Id = s.Id,
            Name = s.Name,
            Active = s.Active,
            CreateDate = s.CreateDate,
            AlteredDate = s.AlteredDate
        }).ToList();
}
