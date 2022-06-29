using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProcessingService
{
	public class Program
	{
		//private static string path = @"C:\D\Learn\FilesResult\";
		private static string queueName = "documment-processing";
		private static string exchangeeName = "document-exch";


		static void Main(string[] args)
		{
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

				Console.ReadKey();
			}
		}

		//private static void ProcessMessage(byte[] message)
		//{
		//	var reciviedMessage = Serializer.Deserialize<Message>(message);
		//	if (reciviedMessage.IsChunk)
		//	{
		//		ProcessChunk(reciviedMessage);
		//	}
		//	else
		//	{
		//		var filePath = $"{path}{reciviedMessage.FileName}";
		//		File.WriteAllBytes(filePath, reciviedMessage.Content);
		//	}

		//	Console.WriteLine($"Received file {reciviedMessage.FileName}");
		//}

		//private static void ProcessChunk(Message chunkMessage)
		//{
		//	if (!Directory.Exists($"{path}{chunkMessage.FileName}"))
		//		Directory.CreateDirectory($"{path}{chunkMessage.FileName}");

		//	if (chunkMessage.IsFinished)
		//	{
		//		CreateFileFromChunkes(chunkMessage, $"{path}{chunkMessage.FileName}");
		//	}
		//	else
		//	{
		//		File.WriteAllBytes($"{path}{chunkMessage.FileName}\\{chunkMessage.Order}", chunkMessage.Content);
		//	}
		//}

		//private static void CreateFileFromChunkes(Message reciviedModel, string chunkPath)
		//{
		//	var messages = new List<Message>();

		//	foreach (string filePath in Directory.EnumerateFiles(path))
		//	{
		//		byte[] contents = File.ReadAllBytes(filePath);

		//		var message = Serializer.Deserialize<Message>(contents);

		//		messages.Add(message);
		//	}

		//	messages = messages.OrderBy(message => message.Order).ToList();
		//	CreateFrileFromMessages(messages, reciviedModel.FileName);
		//}

		//private static void CreateFrileFromMessages(List<Message> messages, string fileName)
		//{
		//	List<byte> content = new List<byte>();
		//	foreach (var message in messages)
		//	{
		//		content.AddRange(message.Content);
		//	}

		//	//var filePathResult = $"{path}{reciviedModel.FileName}.{reciviedModel.Type}";
		//	var filePathResult = $"{path}{messages}";

		//	File.WriteAllBytes(filePathResult, content.ToArray());
		//}
	}
}
