using Sellius.API.Application.DTOs.RegisterDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.EnterpriseServices.CommandServices.Interfaces;
using Sellius.API.Application.Services.UserServices.CommandServices.Interfaces;
using Sellius.API.Domain.Extensions;
using Sellius.API.Infra.Repository.Empresa.Interfaces;

namespace Sellius.API.Application.Services.EnterpriseServices.CommandServices;

public sealed class EnterpriseCommandService(
    IUserCommandService userCommandService,
    IEnterpriseRepository repository,
    IEnterpriseMapper mapper) : IEnterpriseCommandService
{
    public async Task<bool> CreateEnterprise(EnterpriseRegister dto)
    {
        if (await EnterpriseAlreadyExists(dto.Document))
            return false;

        var enterprise = mapper.DtoRegisterToMain(dto);
        enterprise.Document = enterprise.Document.Hash();
        enterprise.CreateDate = DateTime.UtcNow;
        enterprise.AlteredDate = DateTime.UtcNow;
        enterprise.Active = 1;

        if(await repository.CreateEnterpriseAsync(enterprise))
            return await userCommandService.CreateUser(dto.UserRegister,enterprise.Id);
        
        return false;
    }

    public async Task<bool> UpdateEnterprise(EnterpriseRegister dto)
    {
        var enterprise = await repository.FindByPredicateAsync(
            e => e.Id == dto.Id);

        if (enterprise is null)
            return false;

        enterprise.Name = dto.Name;
        enterprise.Phone = dto.Phone;
        enterprise.Email = dto.Email;
        enterprise.CityId = dto.CityId;
        enterprise.ZipCode = dto.ZipCode;
        enterprise.Street = dto.Street;
        enterprise.AlteredDate = DateTime.UtcNow;

        return await repository.UpdateEnterpriseAsync(enterprise);
    }

    public async Task<bool> InactiveEnterprise(Guid enterpriseId)
    {
        var enterprise = await repository.FindByPredicateAsync(
            e => e.Id == enterpriseId);

        if (enterprise is null)
            return false;

        enterprise.Active = 0;
        enterprise.AlteredDate = DateTime.UtcNow;

        return await repository.UpdateEnterpriseAsync(enterprise);
    }

    private async Task<bool> EnterpriseAlreadyExists(string document)
    {
        var existing = await repository.FindByPredicateAsync(
            e => e.Document == document);

        return existing is not null;
    }
}
