using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sellius.API.Domain.Entity.EntityUsers;
using Sellius.API.Infra.Context.ConfigurationContext.Abstract;

namespace Sellius.API.Infra.Context.ConfigurationContext.ConfigurationUser;

public class UserConfigContext: BaseDateName,IEntityTypeConfiguration<UserConfiguration>
{
    public void Configure(EntityTypeBuilder<UserConfiguration> builder)
    {
        builder.ToTable("UserConfigurations", DataBaseName);
        
        
    }
}