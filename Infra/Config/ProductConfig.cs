using Core.Entites;
using Microsoft.EntityFrameworkCore;

namespace Infra.Config;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
    {
       builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
       // builder.Property(p => p.Name).IsRequired();
    }

}
