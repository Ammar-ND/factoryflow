using FactoryFlow.Domain.Common;
using MediatR;

namespace FactoryFlow.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}