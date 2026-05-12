using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sellius.API.Domain.Entity;
using Sellius.API.Infra.Context.ConfigurationContext.Abstract;

namespace Sellius.API.Infra.Context.ConfigurationContext.Supplier;

public class SupplierXCustomerConfigurationContext : BaseDateName, IEntityTypeConfiguration<SupplierXCustomer>
{
    public void Configure(EntityTypeBuilder<SupplierXCustomer> builder)
    {
        builder.ToTable("SupplierXCustomers",DataBaseName);
        
        builder.HasKey(e => new { e.CustomerId , e.SupplierId });
        
        builder.HasOne(fxc => fxc.Customer)
            .WithMany(fxc => fxc.SupplierXCustomer)
            .HasForeignKey(fxc=> fxc.CustomerId);
        
        builder.HasOne(fxc => fxc.Supplier)
            .WithMany(fxc => fxc.SupplierXCustomer)
            .HasForeignKey(fxc=> fxc.SupplierId);


    }
}