using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProcessingService
{
	public class Program
	{
		private static string queueName = "documment-processing";
		private static string exchangeeName = "document-exch";


		static void Main(string[] args)
		{
			// improve: move to separate project
			var factory = new ConnectionFactory() { HostName = "localhost" };
			using (var connection = factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				channel.ExchangeDeclare(exchange: exchangeeName, ExchangeType.Fanout, true);
				channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
				channel.QueueBind(queue: queueName, exchange: exchangeeName, routingKey: String.Empty);

				var consumer = new EventingBasicConsumer(channel);

				consumer.Received += (sender, eventArgs) =>
				{
					var message = eventArgs.Body.ToArray();
					Processor.ProcessMessage(message);
				};

				channel.BasicConsume(queue: queueName, true, consumer);
			}
		}
	}
}
