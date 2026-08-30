using FactoryFlow.Domain.Products;
using FactoryFlow.Domain.ProductionOrders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FactoryFlow.Infrastructure.Persistence.Configurations;

public sealed class ProductionOrderConfiguration
    : IEntityTypeConfiguration<ProductionOrder>
{
    public void Configure(EntityTypeBuilder<ProductionOrder> builder)
    {
        builder.ToTable("ProductionOrders");

        builder.HasKey(productionOrder => productionOrder.Id);

        builder.Property(productionOrder => productionOrder.Id)
            .HasConversion(
                productionOrderId => productionOrderId.Value,
                value => new ProductionOrderId(value))
            .ValueGeneratedNever();

        builder.Property(productionOrder => productionOrder.ProductId)
            .HasConversion(
                productId => productId.Value,
                value => new ProductId(value))
            .IsRequired();

        builder.Property(productionOrder => productionOrder.Quantity)
            .IsRequired();

        builder.Property(productionOrder => productionOrder.Status)
            .IsRequired();

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(productionOrder => productionOrder.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}