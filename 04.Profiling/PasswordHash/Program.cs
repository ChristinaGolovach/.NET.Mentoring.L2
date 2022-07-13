using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace PasswordHash
{
	public class Program
	{
		private static readonly int iterateCount = 10000;
		private static readonly int hashLength = 36;
		private static readonly int keyLength = 20;

		static void Main(string[] args)
		{
			var password = "qwerty";
			var salt = Encoding.ASCII.GetBytes("rm4fSDh0sofK6154");
			for (int i = 0; i < 100; i++)
			{

				GeneratePasswordHashUsingSalt(password, salt);
			}

			for (int i = 0; i < 100; i++)
			{

				GeneratePasswordHashUsingSaltChangedVersion(password, salt);
			}

		}

		public static string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
		{

			var iterate = 10000;
			var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);
			byte[] hash = pbkdf2.GetBytes(20);

			byte[] hashBytes = new byte[36];

			//Stopwatch arrayCoppyTime = new Stopwatch();
			//arrayCoppyTime.Start();

			Array.Copy(salt, 0, hashBytes, 0, 16);
			Array.Copy(hash, 0, hashBytes, 16, 20);

			//arrayCoppyTime.Stop();
			//var arrayCoppyTimeResult = arrayCoppyTime.ElapsedTicks;

			var passwordHash = Convert.ToBase64String(hashBytes);

			return passwordHash;
		}

		public static string GeneratePasswordHashUsingSaltChangedVersion(string passwordText, byte[] salt)
		{
			byte[] hash = null;
			using (var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterateCount))
			{
				hash = pbkdf2.GetBytes(keyLength);
			}

			byte[] hashBytes = new byte[hashLength];
			//Span<byte> hashBytes = stackalloc byte[hashLength];

			//Stopwatch bufferCopyTime = new Stopwatch();
			//bufferCopyTime.Start();

			Buffer.BlockCopy(salt, 0, hashBytes, 0, salt.Length);
			Buffer.BlockCopy(hash, 0, hashBytes, 0, hash.Length);

			//bufferCopyTime.Stop();
			//var rbufferCopyTimeResult = bufferCopyTime.ElapsedTicks;

			var passwordHash = Convert.ToBase64String(hashBytes);

			return passwordHash;
		}
	}
}
