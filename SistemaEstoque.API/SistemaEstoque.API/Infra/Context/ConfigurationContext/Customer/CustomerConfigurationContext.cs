using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sellius.API.Infra.Context.ConfigurationContext.Abstract;

namespace Sellius.API.Infra.Context.ConfigurationContext.Customer;

public class CustomerConfigurationContext : BaseDateName, IEntityTypeConfiguration<Domain.Entity.EntityCustomers.Customer>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.EntityCustomers.Customer> builder)
    {
        builder.ToTable("Customers", DataBaseName);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(c => c.City).WithMany().HasForeignKey(c => c.CityId);
        builder.HasOne(p => p.Enterprise).WithMany().HasForeignKey(c => c.EnterpriseId);
        builder.HasOne(g => g.Gruop).WithMany().HasForeignKey(g => g.GroupId);
        builder.HasOne(s => s.Segmentation).WithMany().HasForeignKey(s => s.SegmentationId);

    }
}