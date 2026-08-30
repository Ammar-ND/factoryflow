using MediatR;

namespace FactoryFlow.Application.Abstractions.Messaging;

public interface ICommand<TResponse> : IRequest<TResponse>
{
}