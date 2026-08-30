using FactoryFlow.Domain.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FactoryFlow.Infrastructure.Persistence.Configurations;

public sealed class FactoryConfiguration : IEntityTypeConfiguration<Factory>
{
    public void Configure(EntityTypeBuilder<Factory> builder)
    {
        builder.ToTable("Factories");

        builder.HasKey(factory => factory.Id);

        builder.Property(factory => factory.Id)
            .HasConversion(
                factoryId => factoryId.Value,
                value => new FactoryId(value))
            .ValueGeneratedNever();

        builder.Property(factory => factory.Name)
            .IsRequired()
            .HasMaxLength(200);
    }
}