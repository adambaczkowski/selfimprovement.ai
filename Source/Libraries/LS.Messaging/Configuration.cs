using LS.Messaging;
using LS.Messaging.EventBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting;

public static class RabbitMqDependencyInjectionExtensions
{
    private const string SectionName = "EventBus";
    
    public static IEventBusBuilder AddRabbitMqEventBus(this IServiceCollection services, IConfiguration configuration, string connectionName)
    {
        services.AddOpenTelemetry()
            .WithTracing(tracing =>
            {
                tracing.AddSource(RabbitMqTelemetry.ActivitySourceName);
            });
        // Options support
        services.Configure<EventBusOptions>(configuration.GetSection(SectionName));

        // Abstractions on top of the core client API
        services.AddSingleton<RabbitMqTelemetry>();
        services.AddSingleton<IEventBus, RabbitMqEventBus>();
        // Start consuming messages as soon as the application starts
        services.AddSingleton<IHostedService>(sp => (RabbitMqEventBus)sp.GetRequiredService<IEventBus>());

        return new EventBusBuilder(services);
    }

    private class EventBusBuilder(IServiceCollection services) : IEventBusBuilder
    {
        public IServiceCollection Services => services;
    }
}