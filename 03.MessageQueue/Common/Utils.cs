using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
	public static class Utils
	{
		
		private const int ChunkSize = 10000;

		public static IList<T[]> GetChunks<T>(T[] importedSource)
		{
			var chunks = new List<T[]>();
			var skip = 0;

			while (true)
			{
				var chunk = importedSource.Skip(skip).Take(ChunkSize).ToArray();
				if (chunk.Length == default(int))
				{
					break;
				}

				chunks.Add(chunk);
				skip += ChunkSize;
			}

			return chunks;
		}
	}
}
