using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sellius.API.Infra.Context.ConfigurationContext.Abstract;
using Sellius.API.Models.Usuario;

namespace Sellius.API.Infra.Context.ConfigurationContext;

public class UserConfigurationContext : BaseDateName,IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.ToTable("User",DataBaseName);

        builder.HasKey(x => x.Id);
        
        builder.HasOne(c => c.Cidade).WithMany().HasForeignKey(c => c.CidadeId);
        builder.HasOne(e => e.Enterprise).WithMany().HasForeignKey(u => u.EnterpriseId);
        builder.HasOne(e => e.TipoUsuario).WithMany().HasForeignKey(e => e.IdTpUsuario);

    }
}