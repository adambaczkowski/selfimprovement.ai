using Microsoft.Extensions.DependencyInjection;

namespace LS.Messaging.EventBus;

public interface IEventBusBuilder
{
    public IServiceCollection Services { get; }
}