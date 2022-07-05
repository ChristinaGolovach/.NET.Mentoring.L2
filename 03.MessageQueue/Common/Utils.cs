using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
	public static class Utils
	{
		private static readonly int chunkSize = 10000;

		public static IList<T[]> GetChunks<T>(T[] importedSource)
		{
			var chunks = new List<T[]>();
			var skip = 0;

			while (true)
			{
				var chunk = importedSource.Skip(skip).Take(chunkSize).ToArray();
				if (chunk.Length == default(int))
				{
					break;
				}

				chunks.Add(chunk);
				skip += chunkSize;
			}

			return chunks;
		}
	}
}
