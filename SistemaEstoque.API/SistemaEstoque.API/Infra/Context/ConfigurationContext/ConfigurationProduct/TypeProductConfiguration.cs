using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sellius.API.Domain.Entity.EntityProduct;
using Sellius.API.Infra.Context.ConfigurationContext.Abstract;

namespace Sellius.API.Infra.Context.ConfigurationContext.ConfigurationProduct;

public class TypeProductConfiguration : BaseDateName, IEntityTypeConfiguration<TypeProduct>
{
    public void Configure(EntityTypeBuilder<TypeProduct> builder)
    {
        builder.ToTable("TypeProducts",DataBaseName);
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(e => e.Enterprise).WithMany().HasForeignKey(e => e.EnterpriseId);

    }
}