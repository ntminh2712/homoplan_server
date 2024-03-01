using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Text;

namespace SeminarAPI.Helpers
{
	public class Encryption
	{
		public static string HashPassword(string password, string salt)
		{
			byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
			byte[] saltBytes = System.Text.Encoding.UTF8.GetBytes(salt);

			string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: Encoding.UTF8.GetString(passwordBytes),
				salt: saltBytes,
				prf: KeyDerivationPrf.HMACSHA256,
				iterationCount: 10000,
				numBytesRequested: 256 / 8));

			return hashed;
		}

		public static bool VerifyPassword(string hashedPassword, string passwordToCheck, string salt)
		{
			return HashPassword(passwordToCheck, salt) == hashedPassword;
		}

		public static string GenerateSalt()
		{
			byte[] randomBytes = new byte[16];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(randomBytes);
			}
			return Convert.ToBase64String(randomBytes);
		}
	}
}
