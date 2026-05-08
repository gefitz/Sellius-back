using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sellius.API.Infra.Context.ConfigurationContext.Abstract;

namespace Sellius.API.Infra.Context.ConfigurationContext.SalesOrder;

public class SaleOrderXProduct : BaseDateName, IEntityTypeConfiguration<SaleOrderXProduct>
{
    public void Configure(EntityTypeBuilder<SaleOrderXProduct> builder)
    {
        builder.ToTable("SaleOrderXProduct",DataBaseName);
        
        builder
    }
}