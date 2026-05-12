using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sellius.API.Domain.Entity.EntityUsers;
using Sellius.API.Infra.Context.ConfigurationContext.Abstract;

namespace Sellius.API.Infra.Context.ConfigurationContext.ConfigurationUser;

public class TypeUserXMenuConfigurationContext : BaseDateName,IEntityTypeConfiguration<TypeUserXMenu>
{
    public void Configure(EntityTypeBuilder<TypeUserXMenu> builder)
    {
        
        builder.ToTable("TypeUserXMenus", DataBaseName);
        
        builder.HasKey(x => new { x.TypeUserId, x.MenuId });

        builder.HasOne(x => x.TypeUser)
            .WithMany(u => u.TypeUserXMenus)
            .HasForeignKey(x => x.TypeUserId);

        builder.HasOne(x => x.Menu)
            .WithMany(m => m.TypeUserXMenus)
            .HasForeignKey(x => x.MenuId);

    }
}