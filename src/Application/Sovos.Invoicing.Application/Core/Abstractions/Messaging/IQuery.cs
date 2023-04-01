using MediatR;

namespace Sovos.Invoicing.Application.Core.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>
{

}
