using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace LS.Messaging;

public class RabbitMqClient : IRabbitMqClient
{
    private readonly ConnectionFactory _connectionFactory =
        new () { HostName = "rabbitmq", Port = 5672, AutomaticRecoveryEnabled = true, DispatchConsumersAsync = true };

    private IConnection _connection;
    private IModel _channel;
    private const string EventsExchangeName = "Events";
    private const int MaxRetryCount = 2;
    private const string QueueNameSettingName = "QueueName";
    private bool _disposed;

    public void CloseConnection()
    {
        _channel?.Close();
        _connection?.Close();
    }

    public async Task PostEvent(string type, object data)
    {
        await EnsureChannelIsOpen();
        _channel.BasicPublish(exchange: EventsExchangeName,
            routingKey: type,
            body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new RabbitMqMessage()
            {
                Message = JsonConvert.SerializeObject(data),
                Type = data.GetType().FullName,
            })));
    }

    public async Task BindEvents(Func<string, object, Task> handler)
    {
        await EnsureChannelIsOpen();
        var queueName = _channel
            .QueueDeclare(Environment.GetEnvironmentVariable(QueueNameSettingName) ?? string.Empty).QueueName;
        _channel.QueueBind(queue: queueName,
            exchange: EventsExchangeName,
            routingKey: string.Empty);
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (_, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var messageString = Encoding.UTF8.GetString(body);
            var routingKey = eventArgs.RoutingKey;
            var message = JsonConvert.DeserializeObject<RabbitMqMessage>(messageString);
            if (message is not null)
            {
                var type = Type.GetType(message.Type);
                if (type is not null)
                {
                    await handler(routingKey, JsonConvert.DeserializeObject(message.Message, type));
                }
            }
        };
        _channel.BasicConsume(queue: queueName,
            autoAck: true,
            consumer: consumer);
    }

    private async Task EnsureChannelIsOpen()
    {
        await EnsureConnectionIsOpen();

        if (_channel is null || _channel.IsClosed)
        {
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: EventsExchangeName,
                type: ExchangeType.Fanout);
        }
    }

    private async Task EnsureConnectionIsOpen()
    {
        if (_connection is null || !_connection.IsOpen)
        {
            for (var retryCount = 0; retryCount <= MaxRetryCount; retryCount++)
            {
                try
                {
                    _connection = _connectionFactory.CreateConnection();
                    break;
                }
                catch (RabbitMQ.Client.Exceptions.BrokerUnreachableException)
                {
                    await Task.Delay(5000);
                    if (retryCount == MaxRetryCount)
                    {
                        throw;
                    }
                }
            }
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}