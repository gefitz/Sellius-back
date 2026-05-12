using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sellius.API.Domain.Entity.EntitysSaleOrder;
using Sellius.API.Infra.Context.ConfigurationContext.Abstract;

namespace Sellius.API.Infra.Context.ConfigurationContext.SalesOrder;

public class SaleOrderXProductConfigurationContext : BaseDateName, IEntityTypeConfiguration<SaleOrdeXProduct>
{
    public void Configure(EntityTypeBuilder<SaleOrdeXProduct> builder)
    {
        builder.ToTable("SaleOrderXProduct",DataBaseName);
        
        builder.HasKey(txp => new {txp.SaleOrderId,txp.ProductId}); 
        
        builder.HasOne(p => p.SaleOrder)
            .WithMany(p => p.Product)
            .HasForeignKey(p => p.SaleOrderId);
        
        builder
            .HasOne(p => p.Product)
            .WithMany(p => p.SaleOrders)
            .HasForeignKey(p => p.ProductId);
    }
}