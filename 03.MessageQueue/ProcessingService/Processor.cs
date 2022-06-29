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

		public static void ProcessMessage(byte[] message)
		{
			var reciviedMessage = Serializer.Deserialize<Message>(message);
			if (reciviedMessage.IsChunk)
			{
				ProcessChunk(reciviedMessage);
			}
			else
			{
				var filePath = $"{path}{reciviedMessage.FileName}";
				File.WriteAllBytes(filePath, reciviedMessage.Content);
			}

			Console.WriteLine($"Received file {reciviedMessage.FileName}");
		}

		private static void ProcessChunk(Message chunkMessage)
		{
			if (!Directory.Exists($"{path}{chunkMessage.FileName}"))
				Directory.CreateDirectory($"{path}{chunkMessage.FileName}");

			if (chunkMessage.IsFinished)
			{
				CreateFileFromChunkes(chunkMessage, $"{path}{chunkMessage.FileName}");
			}
			else
			{
				File.WriteAllBytes($"{path}{chunkMessage.FileName}\\{chunkMessage.Order}", chunkMessage.Content);
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

			//var filePathResult = $"{path}{reciviedModel.FileName}.{reciviedModel.Type}";
			var filePathResult = $"{path}{messages}";

			File.WriteAllBytes(filePathResult, content.ToArray());
		}
	}
}
