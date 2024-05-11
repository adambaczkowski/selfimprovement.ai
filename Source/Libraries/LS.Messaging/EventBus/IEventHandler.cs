﻿namespace LS.Messaging.EventBus;

public interface IEventHandler<in TEvent>
    where TEvent : Event
{
    Task HandleAsync(TEvent @event);
}