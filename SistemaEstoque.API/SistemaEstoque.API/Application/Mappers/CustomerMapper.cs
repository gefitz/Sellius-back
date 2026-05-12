using Sellius.API.Application.DTOs.EditDTOs;
using Sellius.API.Application.DTOs.RegisterDTOs.CustomerRegisterDTOs;
using Sellius.API.Application.DTOs.TablesDTOs;
using Sellius.API.Application.Mappers.Interfaces;
using Sellius.API.Domain.Entity.EntityCustomers;

namespace Sellius.API.Application.Mappers;

public sealed class CustomerMapper : ICustomerMapper
{
    public Customer DtoRegisterToMain(CustomerRegister dto, Guid enterpriseId) =>
        new Customer
        {
            Name = dto.Name,
            Document = dto.Document,
            CityId = dto.CityId,
            Street = dto.Street,
            Neighborhood = dto.Neighborhood,
            ZipCode = dto.ZipCode,
            Email = dto.Email,
            Phone = dto.Phone,
            Active = dto.Active,
            SegmentationId = dto.SegmentionId,
            GroupId = (int?)dto.GroupId,
            EnterpriseId = enterpriseId,
            CreateDate = DateTime.UtcNow,
            AlteredDate = DateTime.UtcNow
        };

    public CustomerEdit MainToDtoEdit(Customer customer) =>
        new CustomerEdit
        {
            Id = customer.Id,
            Name = customer.Name,
            Document = customer.Document,
            CityId = customer.CityId,
            Street = customer.Street,
            Neighborhood = customer.Neighborhood,
            ZipCode = customer.ZipCode,
            Email = customer.Email,
            Phone = customer.Phone,
            Active = customer.Active,
            SegmentationId = customer.SegmentationId,
            GroupId = customer.GroupId,
            EnterpriseId = customer.EnterpriseId,
            CreateDate = customer.CreateDate,
            AlteredDate = customer.AlteredDate
        };

    public List<CustomerTableReturn> MainToTableList(List<Customer> customers) =>
        customers.Select(c => new CustomerTableReturn
        {
            Id = c.Id,
            Name = c.Name,
            Document = c.Document,
            Phone = c.Phone,
            Email = c.Email,
            City = c.City?.Name ?? string.Empty,
            Street = c.Street,
            CreateDate = c.CreateDate,
            AlteredDate = c.AlteredDate,
            Active = c.Active,
            Group = c.Gruop?.Name,
            Segmentation = c.Segmentation?.Name
        }).ToList();
}
