using System;
using System.IO;
using System.Threading.Tasks;
using Common;
using Models;
using RabbitMQ.Client;

namespace DataCaptureService
{
	public class Program
	{
		private static string sourceFolderName = @"C:\D\Learn\Files";
		private static string backupFolderName = @"C:\D\Learn\FilesBackup\";
		private static string fileFormat = "*.pdf";
		private static string exchangeName = "document-exch";
		private static int maxFileSizeByte = 1048576;
		private static Guid ServiceId = Guid.NewGuid();

		static async Task Main(string[] args)
		{
			while (true)
			{
				foreach (string filePath in Directory.EnumerateFiles(sourceFolderName, fileFormat))
				{
					var fileInfo = new FileInfo(filePath);
					byte[] contents = File.ReadAllBytes(filePath);

					if (fileInfo.Length > maxFileSizeByte)
					{
						ProcessLargeFile(fileInfo, contents);
					}
					else
					{
						var model = new Message()
						{
							Id = ServiceId,
							FileName = fileInfo.Name,
							Type = fileInfo.Extension,
							Content = contents
						};

						SendFile(model);
					}

					Console.WriteLine($"Sended file is {fileInfo.Name}");

					File.Move(filePath, $"{backupFolderName}{fileInfo.Name}", true);
				}

				await Task.Delay(2000);
			}
		}

		private static void ProcessLargeFile(FileInfo fileInfo, byte[] contents)
		{
			var model = new Message()
			{
				Id = ServiceId,
				FileName = fileInfo.Name,
				Type = fileInfo.Extension,
				Length = fileInfo.Length,
				IsChunk = true
			};

			var chunks = Utils.GetChunks(contents);

			foreach (var chunk in chunks)
			{
				model.Content = chunk;
				model.Order++;

				if (chunks.Count == model.Order)
				{
					model.IsFinished = true;
				}

				SendFile(model);
			}
		}

		private static void SendFile(Message model)
		{
			var factory = new ConnectionFactory() { HostName = "localhost" };
			using (var connection = factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				channel.ExchangeDeclare(exchange: exchangeName, ExchangeType.Fanout, true);

				var result = Serializer.Serialize(model);

				channel.BasicPublish(exchange: exchangeName, routingKey: "", basicProperties: null, body: result);
			}
		}
	}
}
