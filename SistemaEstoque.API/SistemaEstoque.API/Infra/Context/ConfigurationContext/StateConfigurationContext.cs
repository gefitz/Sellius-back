using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sellius.API.Domain.Entity;
using Sellius.API.Infra.Context.ConfigurationContext.Abstract;

namespace Sellius.API.Infra.Context.ConfigurationContext;

public class StateConfigurationContext : BaseDateName, IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.ToTable("States", DataBaseName);

        builder.HasKey(x => x.Id);
        builder.Property(x=>x.Id).ValueGeneratedOnAdd();
    }
}