using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sellius.API.Domain.Entity.EntityUsers;
using Sellius.API.Infra.Context.ConfigurationContext.Abstract;

namespace Sellius.API.Infra.Context.ConfigurationContext.ConfigurationUser;

public class MenuConfigurationContext : BaseDateName, IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("Menus",DataBaseName);
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

    }
}