using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sellius.API.Domain.Entity.EntityCustomers;
using Sellius.API.Infra.Context.ConfigurationContext.Abstract;

namespace Sellius.API.Infra.Context.ConfigurationContext.Customer;

public class SegmentationConfigurationContext : BaseDateName, IEntityTypeConfiguration<Segmentation>
{
    public void Configure(EntityTypeBuilder<Segmentation> builder)
    {
        builder.ToTable("Segmentations",DataBaseName);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasOne(e => e.Enterprise).WithMany().HasForeignKey(e => e.EnterpriseId);

    }
}