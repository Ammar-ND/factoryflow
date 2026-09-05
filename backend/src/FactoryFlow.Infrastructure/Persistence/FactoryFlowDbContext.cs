using FactoryFlow.Domain.Factories;
using FactoryFlow.Domain.Machines;
using FactoryFlow.Domain.Products;
using FactoryFlow.Domain.ProductionLines;
using FactoryFlow.Domain.ProductionOrders;
using Microsoft.EntityFrameworkCore;
using FactoryFlow.Application.Abstractions.Persistence;

namespace FactoryFlow.Infrastructure.Persistence;

public sealed class FactoryFlowDbContext : DbContext, IUnitOfWork
{
    public FactoryFlowDbContext(
        DbContextOptions<FactoryFlowDbContext> options)
        : base(options)
    {
    }

    public DbSet<Factory> Factories => Set<Factory>();

    public DbSet<ProductionLine> ProductionLines => Set<ProductionLine>();

    public DbSet<Machine> Machines => Set<Machine>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<ProductionOrder> ProductionOrders => Set<ProductionOrder>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(FactoryFlowDbContext).Assembly);
    }
}