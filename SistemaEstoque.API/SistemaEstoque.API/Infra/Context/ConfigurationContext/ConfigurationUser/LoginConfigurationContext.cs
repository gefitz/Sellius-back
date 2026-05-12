using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sellius.API.Domain.Entity.EntityUsers;
using Sellius.API.Infra.Context.ConfigurationContext.Abstract;

namespace Sellius.API.Infra.Context.ConfigurationContext.ConfigurationUser;

public class LoginConfigurationContext : BaseDateName, IEntityTypeConfiguration<Authentication>
{
    public void Configure(EntityTypeBuilder<Authentication> builder)
    {
        builder.ToTable("Logins", DataBaseName);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(l => l.User)
            .WithMany()
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(l => l.Enterprise)
            .WithMany()
            .HasForeignKey(l => l.EnterpriseId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}