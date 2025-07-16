using Azure.Messaging.ServiceBus;
using System.Text.Json;

namespace OrdersApi.Services
{
    public class MessageBusService
    {
        private readonly string _connectionString;
        private readonly string _queueName;

        public MessageBusService(IConfiguration configuration)
        {
            _connectionString = configuration["ServiceBus:ConnectionString"];
            _queueName = configuration["ServiceBus:QueueName"];
        }

        public async Task SendMessageAsync<T>(T obj)
        {
            string messageBody = JsonSerializer.Serialize(obj);

            await using var client = new ServiceBusClient(_connectionString);
            ServiceBusSender sender = client.CreateSender(_queueName);

            var serviceBusMessage = new ServiceBusMessage(messageBody);
            await sender.SendMessageAsync(serviceBusMessage);
        }

    }
}
