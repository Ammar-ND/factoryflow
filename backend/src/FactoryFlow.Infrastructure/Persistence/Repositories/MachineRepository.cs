using FactoryFlow.Application.Abstractions.Persistence;
using FactoryFlow.Domain.Machines;

namespace FactoryFlow.Infrastructure.Persistence.Repositories;

public sealed class MachineRepository : IMachineRepository
{
    private readonly FactoryFlowDbContext _dbContext;

    public MachineRepository(FactoryFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        Machine machine,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Machines.AddAsync(
            machine,
            cancellationToken);
    }
}