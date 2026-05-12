using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sellius.API.Domain.Entity.EntityProduct;
using Sellius.API.Infra.Context.ConfigurationContext.Abstract;

namespace Sellius.API.Infra.Context.ConfigurationContext.ConfigurationProduct;

public class PriceTableXProductConfigurationContext : BaseDateName, IEntityTypeConfiguration<PriceTableXProduct>
{
    public void Configure(EntityTypeBuilder<PriceTableXProduct> builder)
    {
        builder.ToTable("PriceTableXProducts",DataBaseName);
        
        builder.HasKey(txp => new {txp.PriceTableId,txp.ProductId});
        
        builder.HasOne(txp => txp.PriceTable)
            .WithMany(t => t.PriceTableXProducts)
            .HasForeignKey(txp => txp.PriceTableId);
        
        builder.HasOne(txp => txp.Produto)
            .WithMany(t => t.PriceTableXProducts)
            .HasForeignKey(txp=>txp.ProductId);

    }
}