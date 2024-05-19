using System.Reflection;
using LS.Messaging.EventBus;
using LS.Messaging.Subscriptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace LS.Messaging;

public static class Configuration
{
    public static void AddRabbitMqEventBus(this IServiceCollection services, string connectionUrl, string brokerName, string queueName, int timeoutBeforeReconnecting = 15)
    {
        services.AddSingleton<IEventBusSubscriptionManager, InMemoryEventBusSubscriptionManager>();
        services.AddSingleton<IPersistentConnection, DefaultRabbitMqPersistentConnection>(factory =>
        {
            var connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(connectionUrl),
                DispatchConsumersAsync = true,
            };

            var logger = factory.GetService<ILogger<DefaultRabbitMqPersistentConnection>>();
            return new DefaultRabbitMqPersistentConnection(connectionFactory, logger, timeoutBeforeReconnecting);
        });

        services.AddSingleton<IEventBus, RabbitMqEventBus>(factory =>
        {
            var persistentConnection = factory.GetService<IPersistentConnection>();
            var subscriptionManager = factory.GetService<IEventBusSubscriptionManager>();
            var logger = factory.GetService<ILogger<RabbitMqEventBus>>();

            return new RabbitMqEventBus(persistentConnection, subscriptionManager, factory, logger, brokerName, queueName);
        });
    }
}