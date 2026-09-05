using FactoryFlow.Domain.Machines;

namespace FactoryFlow.Application.Abstractions.Persistence;

public interface IMachineRepository
{
    Task AddAsync(
        Machine machine,
        CancellationToken cancellationToken = default);
}