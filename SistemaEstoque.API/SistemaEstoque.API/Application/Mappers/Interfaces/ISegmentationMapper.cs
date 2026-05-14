using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Domain.Entity.EntityCustomers;

namespace Sellius.API.Application.Mappers.Interfaces;

public interface ISegmentationMapper
{
    Segmentation DtoRegisterToMain(SegmentationRegister dto, Guid enterpriseId);
    SegmentationEdit MainToDtoEdit(Segmentation segmentation);
    List<SegmentationTableReturn> MainToTableList(List<Segmentation> segmentations);
}
