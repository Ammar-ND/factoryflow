using FactoryFlow.Domain.Common;
using MediatR;

namespace FactoryFlow.Application.Abstractions.Messaging;

public interface ICommandHandler<TCommand>
    : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}