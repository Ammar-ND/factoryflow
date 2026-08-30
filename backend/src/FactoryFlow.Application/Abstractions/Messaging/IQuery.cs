using MediatR;

namespace FactoryFlow.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<TResponse>
{
}