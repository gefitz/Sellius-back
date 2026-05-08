using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sellius.API.Infra.Context.ConfigurationContext.Abstract;
using Sellius.API.Models.Usuario;

namespace Sellius.API.Infra.Context.ConfigurationContext;

public class UserConfigContext: BaseDateName,IEntityTypeConfiguration<UserConfiguration>
{
    public void Configure(EntityTypeBuilder<UserConfiguration> builder)
    {
        builder.ToTable("UserConfigurations", DataBaseName);
        
        
    }
}