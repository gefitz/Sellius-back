using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.EnterpriseServices.QueryServices.Interfaces;
using Sellius.API.Infra.Repository.Empresa.Interfaces;

namespace Sellius.API.Application.Services.EnterpriseServices.QueryServices;

public sealed class EnterpriseQueryService(
    IEmpresaRepository repository,
    IEnterpriseMapper mapper) : IEnterpriseQueryService
{
    public async Task<EnterpriseEdit> FindByEnterpriseId(Guid enterpriseId)
    {
        var enterprise = await repository.FindByPredicateAsync(
            e => e.Id == enterpriseId);

        return enterprise is not null ? mapper.MainToDtoEdit(enterprise) : new EnterpriseEdit();
    }

    public async Task<List<EnterpriseRegister>> FindAllEnterprises()
    {
        var enterprises = await repository.FindAllAsync(
            e => e.Active == 1,
            null,
            o => o.OrderBy(e => e.Name));

        return enterprises.Select(e => new EnterpriseRegister
        {
            Name = e.Name,
            Document = e.Document,
            Phone = e.Phone,
            Email = e.Email,
            ZipCode = e.ZipCode,
            Street = e.Street,
            CreateDate = e.CreateDate,
            AlteredDate = e.AlteredDate,
            Active = e.Active
        }).ToList();
    }
}
