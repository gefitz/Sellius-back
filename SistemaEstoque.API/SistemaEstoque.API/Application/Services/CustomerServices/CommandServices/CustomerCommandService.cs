using Sellius.API.Application.DTOs.RegisterDTOs.CustomerRegisterDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Application.Services.CustomerServices.CommandServices.Interfaces;
using Sellius.API.Domain.Extensions;
using Sellius.API.Infra.Repository.Cliente.Interfaces;

namespace Sellius.API.Application.Services.CustomerServices.CommandServices;

public sealed class CustomerCommandService(
    ICustomerRepository repository,
    ICustomerMapper mapper) : ICustomerCommandService
{
    public async Task<bool> CreateCustomer(CustomerRegister dto, Guid enterpriseId)
    {
        if (await CustomerAlreadyExists(dto.Document, enterpriseId))
            return false;

        var customer = mapper.DtoRegisterToMain(dto, enterpriseId);
        customer.Document = customer.Document.Hash();
        customer.CreateDate = DateTime.UtcNow;
        customer.AlteredDate = DateTime.UtcNow;
        customer.Active = 1;

        return await repository.CreateCustomerAsync(customer);
    }

    public async Task<bool> UpdateCustomer(CustomerRegister dto)
    {
        var customer = await repository.FindByPredicateAsync(
            c => c.Id == dto.Id);

        if (customer is null)
            return false;

        customer.Name = dto.Name;
        customer.Document = dto.Document.Hash();
        customer.CityId = dto.CityId;
        customer.Street = dto.Street;
        customer.Neighborhood = dto.Neighborhood;
        customer.ZipCode = dto.ZipCode;
        customer.Email = dto.Email;
        customer.Phone = dto.Phone;
        customer.SegmentationId = dto.SegmentionId;
        customer.GroupId = dto.GroupId;
        customer.AlteredDate = DateTime.UtcNow;

        return await repository.UpdateCustomerAsync(customer);
    }

    public async Task<bool> InactiveCustomer(long customerId)
    {
        var customer = await repository.FindByPredicateAsync(
            c => c.Id == customerId);

        if (customer is null)
            return false;

        customer.Active = 0;
        customer.AlteredDate = DateTime.UtcNow;

        return await repository.UpdateCustomerAsync(customer);
    }

    private async Task<bool> CustomerAlreadyExists(string document, Guid enterpriseId)
    {
        var existing = await repository.FindByPredicateAsync(
            c => c.Document == document && c.EnterpriseId == enterpriseId);

        return existing is not null;
    }
}
