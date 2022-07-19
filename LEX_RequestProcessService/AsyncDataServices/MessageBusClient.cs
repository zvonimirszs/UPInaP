using System.Text;
using System.Text.Json;
using LEX_RequestProcessService.Dtos;
using RabbitMQ.Client;

namespace LEX_RequestProcessService.AsyncDataServices;

/// <summary>
/// Client klasa - slanje poruka
/// </summary>
public class MessageBusClient : IMessageBusClient
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public MessageBusClient(IConfiguration configuration)
    {
        _configuration = configuration;
        var factory = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMQHost"],
            Port = int.Parse(_configuration["RabbitMQPort"])
        };
        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: _configuration["RabbitExchange"], type: ExchangeType.Fanout);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

             Console.WriteLine("--> Connected to MessageBus");

        }
        catch (Exception ex)
        {
                Console.WriteLine($"--> Could not connect to the Message Bus: {ex.Message}");
        }
    }
    /// <summary>
    /// Slanje poruka
    /// </summary>
    /// <param name="message">poruka koja se šalje</param>
    /// <param name="rabbitExchange">koji je exchange na koji se šalje</param>
    private void SendMessage(string message, string rabbitExchange)
    {
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: rabbitExchange,
                        routingKey: "",
                        basicProperties: null,
                        body: body);
        Console.WriteLine($"--> We have sent {message}");
    }

    public void Dispose()
    {
        Console.WriteLine("MessageBus Disposed");
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
    }

    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
    {
        Console.WriteLine("--> RabbitMQ Connection Shutdown");
    }
    /// <summary>
    /// Publiciranje novog zahtjeva (request)
    /// </summary>
    /// <param name="requestPublishedDto">klasa sa podacima</param>
    public void PublishNewRequestProcess(RequestPublishedDto requestPublishedDto)
    {
        var message = JsonSerializer.Serialize(requestPublishedDto);

        if (_connection.IsOpen)
        {
            Console.WriteLine("--> RabbitMQ Connection Open, sending message...");
            SendMessage(message,_configuration["RabbitExchange"]);
        }
        else
        {
            Console.WriteLine("--> RabbitMQ connectionis closed, not sending");
        } 
    }
}