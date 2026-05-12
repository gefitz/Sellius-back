using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sellius.API.Domain.Entity.EntityUsers;
using Sellius.API.Infra.Context.ConfigurationContext.Abstract;

namespace Sellius.API.Infra.Context.ConfigurationContext.ConfigurationUser;

public class UserConfigurationContext : BaseDateName,IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User",DataBaseName);

        builder.HasKey(x => x.Id);
        
        builder.HasOne(c => c.City).WithMany().HasForeignKey(c => c.CityId);
        builder.HasOne(e => e.Enterprise).WithMany().HasForeignKey(u => u.EnterpriseId);
        builder.HasOne(e => e.TypeUser).WithMany().HasForeignKey(e => e.TpUsuarioId);

    }
}