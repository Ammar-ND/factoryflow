using FactoryFlow.Domain.Machines;
using FactoryFlow.Domain.ProductionLines;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FactoryFlow.Infrastructure.Persistence.Configurations;

public sealed class MachineConfiguration
    : IEntityTypeConfiguration<Machine>
{
    public void Configure(EntityTypeBuilder<Machine> builder)
    {
        builder.ToTable("Machines");

        builder.HasKey(machine => machine.Id);

        builder.Property(machine => machine.Id)
            .HasConversion(
                machineId => machineId.Value,
                value => new MachineId(value))
            .ValueGeneratedNever();

        builder.Property(machine => machine.ProductionLineId)
            .HasConversion(
                productionLineId => productionLineId.Value,
                value => new ProductionLineId(value))
            .IsRequired();

        builder.Property(machine => machine.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(machine => machine.Status)
            .IsRequired();

        builder.HasOne<ProductionLine>()
            .WithMany()
            .HasForeignKey(machine => machine.ProductionLineId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}