using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sellius.API.Infra.Context.ConfigurationContext.Abstract;
using Sellius.API.Models.Usuario;

namespace Sellius.API.Infra.Context.ConfigurationContext;

public class TypeUserXMenuConfigurationContext : BaseDateName,IEntityTypeConfiguration<TypeUserXMenu>
{
    public void Configure(EntityTypeBuilder<TypeUserXMenu> builder)
    {
        
        builder.ToTable("TypeUserXMenus", DataBaseName);
        
        builder.HasKey(x => new { x.idTpUsuario, x.idMenu });

        builder.HasOne(x => x.tpUsuario)
            .WithMany(u => u.TpUsuarioXMenus)
            .HasForeignKey(x => x.idTpUsuario);

        builder.HasOne(x => x.Menu)
            .WithMany(m => m.TpUsuarioXMenus)
            .HasForeignKey(x => x.idMenu);

    }
}