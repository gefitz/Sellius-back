using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sellius.API.Domain.Entity.EntityProduct;
using Sellius.API.Infra.Context.ConfigurationContext.Abstract;

namespace Sellius.API.Infra.Context.ConfigurationContext.ConfigurationProduct;

public class PriceTableConfigurationContext: BaseDateName, IEntityTypeConfiguration<PriceTable>
{
    public void Configure(EntityTypeBuilder<PriceTable> builder)
    {
        builder.ToTable("PriceTables",DataBaseName);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(e => e.Enterprise).WithMany().HasForeignKey(e => e.EnterpriseId);
        
        builder.HasOne(f => f.Supplier).WithMany().HasForeignKey(f => f.SupplierId);

    }
}