using System.Diagnostics;
using OpenTelemetry.Context.Propagation;

namespace LS.Messaging;

public class RabbitMqTelemetry
{
    public static string ActivitySourceName = "EventBusRabbitMQ";

    public ActivitySource ActivitySource { get; } = new(ActivitySourceName);
    public TextMapPropagator Propagator { get; } = Propagators.DefaultTextMapPropagator;
}