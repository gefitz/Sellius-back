using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sellius.API.Domain.Entity.EntityCustomers;
using Sellius.API.Infra.Context.ConfigurationContext.Abstract;

namespace Sellius.API.Infra.Context.ConfigurationContext.Customer;

public class GroupCustomerConfigurationContext : BaseDateName, IEntityTypeConfiguration<GroupCustomer>
{
    public void Configure(EntityTypeBuilder<GroupCustomer> builder)
    {
        builder.ToTable("GroupCustomers",DataBaseName);

        builder.HasKey(x => x.Id);
        
        builder.Property(x=> x.Id).ValueGeneratedOnAdd();

        builder.HasOne(e => e.Enterprise).WithMany().HasForeignKey(e => e.EnterpriseId);

    }
}