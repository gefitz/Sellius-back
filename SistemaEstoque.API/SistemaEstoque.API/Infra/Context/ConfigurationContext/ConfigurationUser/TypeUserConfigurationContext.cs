using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sellius.API.Infra.Context.ConfigurationContext.Abstract;
using Sellius.API.Models.Usuario;

namespace Sellius.API.Infra.Context.ConfigurationContext;

public class TypeUserConfigurationContext : BaseDateName,IEntityTypeConfiguration<TypeUser>
{
    public void Configure(EntityTypeBuilder<TypeUser> builder)
    {
        builder.ToTable("TypeUsers", DataBaseName);

        builder.HasKey(x => x.Id);
        
        builder.HasOne(e => e.Enterprise).WithMany().HasForeignKey(e => e.EnterpriseId);
        
    }
}