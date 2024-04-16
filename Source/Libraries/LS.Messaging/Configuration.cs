using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LS.Messaging;

public static class Configuration
{
    public static void AddMassTransitBus(this IServiceCollection container,
        IConfiguration configuration,
        IEnumerable<Assembly> assemblies)
    {
        var serviceName = configuration["Service"].Split('.').First()
            .Replace("http://", string.Empty)
            .Replace("https://", string.Empty);

        container.AddMassTransit(x =>
        {
            foreach (var assembly in assemblies)
            {
                x.AddConsumers(assembly);
            }
            x.AddDelayedMessageScheduler();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint(serviceName, y =>
                    {
                        y.Durable = true;

                        y.ConfigureConsumers(context);
                    });
                });
        });
    }
}