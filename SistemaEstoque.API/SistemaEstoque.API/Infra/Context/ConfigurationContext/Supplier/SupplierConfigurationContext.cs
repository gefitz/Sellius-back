using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sellius.API.Infra.Context.ConfigurationContext.Abstract;
using Sellius.API.Models;

namespace Sellius.API.Infra.Context.ConfigurationContext;

public class SupplierConfigurationContext : BaseDateName, IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("Suppliers",DataBaseName);

        builder.HasKey(x => x.Id);
        
        builder.Property(x=> x.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(e => e.Enterprise).WithMany().HasForeignKey(f => f.EnterpriseId);
        builder.HasOne(e => e.City).WithMany().HasForeignKey(f => f.CityId);

    }
}