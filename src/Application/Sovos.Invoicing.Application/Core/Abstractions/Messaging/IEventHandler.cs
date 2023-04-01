using MediatR;

namespace Sovos.Invoicing.Application.Core.Abstractions.Messaging;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : INotification
{

}