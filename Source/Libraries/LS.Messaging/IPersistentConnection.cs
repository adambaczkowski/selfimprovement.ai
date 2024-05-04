using RabbitMQ.Client;

namespace LS.Messaging;

public interface IPersistentConnection
{
    event EventHandler OnReconnectedAfterConnectionFailure;
    bool IsConnected { get; }

    bool TryConnect();
    IModel CreateModel();
}