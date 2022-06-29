using System;

namespace Models
{
	[Serializable]

	public class Message
	{
		public Guid Id { get; set; }
		public string FileName { get; set; }
		public string Type { get; set; }
		public byte[] Content { get; set; }
		public bool IsChunk { get; set; }
		public int Order { get; set; }
		public bool IsFinished { get; set; }
		public long Length { get; set; }
	}
}
