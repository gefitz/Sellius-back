using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sellius.API.Domain.Entity.EntitysSaleOrder;
using Sellius.API.Infra.Context.ConfigurationContext.Abstract;

namespace Sellius.API.Infra.Context.ConfigurationContext.SalesOrder;

public class SalesOrderConfigurationContext : BaseDateName, IEntityTypeConfiguration<SaleOrder>
{
    public void Configure(EntityTypeBuilder<SaleOrder> builder)
    {
        builder.ToTable("SaleOrders",DataBaseName);
        
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(c => c.Customer).WithMany(c => c.SaleOrders).HasForeignKey(p => p.CustomerId);
        builder.HasOne(u => u.User).WithMany(u => u.SaleOrder).HasForeignKey(p => p.UserId);
        builder.HasOne(e => e.Enterprise).WithMany().HasForeignKey(p => p.EnterpriseId);
    }
}