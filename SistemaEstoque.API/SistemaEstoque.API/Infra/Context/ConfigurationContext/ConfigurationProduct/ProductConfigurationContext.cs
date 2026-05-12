using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sellius.API.Domain.Entity.EntityProduct;
using Sellius.API.Infra.Context.ConfigurationContext.Abstract;

namespace Sellius.API.Infra.Context.ConfigurationContext.ConfigurationProduct;

public class ProductConfigurationContext : BaseDateName,IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products", DataBaseName);
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(tp => tp.TypeProduct).WithMany(tp => tp.Products).HasForeignKey(p => p.TypeProductId);
        builder.HasOne(e => e.Enterprise).WithMany().HasForeignKey(p => p.EnterpriseId);
        builder.HasOne(f => f.Supplier).WithMany().HasForeignKey(f => f.SupplierId);

    }
}