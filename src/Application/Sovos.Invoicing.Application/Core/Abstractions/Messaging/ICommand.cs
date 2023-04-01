using MediatR;

namespace Sovos.Invoicing.Application.Core.Abstractions.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>
{

}
