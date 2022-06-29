using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Common
{
	public static class Serializer
	{
		public static byte[] Serialize<T>(T model)
		{
			byte[] bytes;

			IFormatter formatter = new BinaryFormatter();

			using (MemoryStream stream = new MemoryStream())
			{
#pragma warning disable SYSLIB0011 // Type or member is obsolete
				formatter.Serialize(stream, model);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
				bytes = stream.ToArray();
			}

			return bytes;
		}

		public static T Deserialize<T>(byte[] content)
		{
			IFormatter formatter = new BinaryFormatter();
			using (MemoryStream stream = new MemoryStream(content))
			{
				//stream.Seek(0, SeekOrigin.Begin);
#pragma warning disable SYSLIB0011 // Type or member is obsolete
				var objectModel = formatter.Deserialize(stream);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
				
				return (T)objectModel;
			}
		}
	}
}
