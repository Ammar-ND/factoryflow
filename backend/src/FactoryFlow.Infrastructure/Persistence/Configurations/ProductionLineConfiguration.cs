using FactoryFlow.Domain.Factories;
using FactoryFlow.Domain.ProductionLines;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FactoryFlow.Infrastructure.Persistence.Configurations;

public sealed class ProductionLineConfiguration
    : IEntityTypeConfiguration<ProductionLine>
{
    public void Configure(EntityTypeBuilder<ProductionLine> builder)
    {
        builder.ToTable("ProductionLines");

        builder.HasKey(productionLine => productionLine.Id);

        builder.Property(productionLine => productionLine.Id)
            .HasConversion(
                productionLineId => productionLineId.Value,
                value => new ProductionLineId(value))
            .ValueGeneratedNever();

        builder.Property(productionLine => productionLine.FactoryId)
            .HasConversion(
                factoryId => factoryId.Value,
                value => new FactoryId(value))
            .IsRequired();

        builder.Property(productionLine => productionLine.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasOne<Factory>()
            .WithMany()
            .HasForeignKey(productionLine => productionLine.FactoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}