using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;
using Models;

namespace ProcessingService
{
	public static class Processor
	{
		private static string path = @"C:\D\Learn\FilesResult\";

		public static void ProcessMessage(byte[] rawMessage)
		{
			var message = Serializer.Deserialize<Message>(rawMessage);
			if (message.IsChunk)
			{
				ProcessChunk(message, rawMessage);
			}
			else
			{
				var filePath = $"{path}{message.FileName}";
				File.WriteAllBytes(filePath, message.Content);
			}

			Console.WriteLine($"Received file {message.FileName}");
		}

		private static void ProcessChunk(Message chunkMessage, byte[] rawMessage)
		{
			var chunkPath = $"{path}chunks\\{chunkMessage.FileName}";

			if (!Directory.Exists(chunkPath))
			{
				Directory.CreateDirectory(chunkPath);
			}

			File.WriteAllBytes($"{chunkPath}\\{chunkMessage.Order}", rawMessage);

			if (chunkMessage.IsFinished)
			{
				CreateFileFromChunkes(chunkMessage, chunkPath);
			}
		}

		private static void CreateFileFromChunkes(Message reciviedModel, string chunkPath)
		{
			var messages = new List<Message>();

			foreach (string filePath in Directory.EnumerateFiles(chunkPath))
			{
				byte[] contents = File.ReadAllBytes(filePath);

				var message = Serializer.Deserialize<Message>(contents);

				messages.Add(message);
			}

			messages = messages.OrderBy(message => message.Order).ToList();
			CreateFrileFromMessages(messages, reciviedModel.FileName);
		}

		private static void CreateFrileFromMessages(List<Message> messages, string fileName)
		{
			List<byte> content = new List<byte>();
			foreach (var message in messages)
			{
				content.AddRange(message.Content);
			}

			var filePathResult = $"{path}{fileName}";

			File.WriteAllBytes(filePathResult, content.ToArray());
		}
	}
}
