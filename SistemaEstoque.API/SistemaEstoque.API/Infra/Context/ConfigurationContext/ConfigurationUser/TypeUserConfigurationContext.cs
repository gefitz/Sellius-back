using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sellius.API.Domain.Entity.EntityUsers;
using Sellius.API.Infra.Context.ConfigurationContext.Abstract;

namespace Sellius.API.Infra.Context.ConfigurationContext.ConfigurationUser;

public class TypeUserConfigurationContext : BaseDateName,IEntityTypeConfiguration<TypeUser>
{
    public void Configure(EntityTypeBuilder<TypeUser> builder)
    {
        builder.ToTable("TypeUsers", DataBaseName);

        builder.HasKey(x => x.Id);
        
        builder.HasOne(e => e.Enterprise).WithMany().HasForeignKey(e => e.EnterpriseId);
        
    }
}