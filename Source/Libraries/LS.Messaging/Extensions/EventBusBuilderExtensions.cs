﻿using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using LS.Messaging.EventBus;
using Microsoft.Extensions.DependencyInjection;

namespace LS.Messaging.Extensions;

public static class EventBusBuilderExtensions
{
    public static IEventBusBuilder ConfigureJsonOptions(this IEventBusBuilder eventBusBuilder, Action<JsonSerializerOptions> configure)
    {
        eventBusBuilder.Services.Configure<EventBusSubscriptionInfo>(o =>
        {
            configure(o.JsonSerializerOptions);
        });

        return eventBusBuilder;
    }

    public static IEventBusBuilder AddSubscription<T, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TH>(this IEventBusBuilder eventBusBuilder)
        where T : Event
        where TH : class, IEventHandler<T>
    {
        // Use keyed services to register multiple handlers for the same event type
        // the consumer can use IKeyedServiceProvider.GetKeyedService<IIntegrationEventHandler>(typeof(T)) to get all
        // handlers for the event type.
        eventBusBuilder.Services.AddKeyedTransient<IEventHandler, TH>(typeof(T));

        eventBusBuilder.Services.Configure<EventBusSubscriptionInfo>(o =>
        {
            // Keep track of all registered event types and their name mapping. We send these event types over the message bus
            // and we don't want to do Type.GetType, so we keep track of the name mapping here.

            // This list will also be used to subscribe to events from the underlying message broker implementation.
            o.EventTypes[typeof(T).Name] = typeof(T);
        });

        return eventBusBuilder;
    }
}