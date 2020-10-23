using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BAI.Adir.Api.Helper
{
    public class EncryptionHelper
    {

		public static string EncryptPassword(string plainPassword, string genSalt)
        {
			return Convert.ToBase64String(GenerateSaltedHash(Encoding.UTF8.GetBytes(plainPassword), Convert.FromBase64String(genSalt)));
		}

		//CompareByteArrays(GenerateSaltedHash(Encoding.UTF8.GetBytes("password"), CreateSalt(3)), passwordhash)
		public static bool CheckPassword(string plainPassword, string dbPasswordHash, string dbSalt)
		{
			//var pass = Convert.FromBase64String(plainPassword);
			byte[] pass = GenerateSaltedHash(Encoding.UTF8.GetBytes(plainPassword), Convert.FromBase64String(dbSalt));
			byte[] dbpass = Convert.FromBase64String(dbPasswordHash);
			if (pass.Length != dbpass.Length)
			{
				return false;
			}

			for (int i = 0; i < pass.Length; i++)
			{
				if (pass[i] != dbpass[i])
				{
					return false;
				}
			}

			return true;
		}

		public static string CreateSalt()
		{
			int size = 15;
			RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
			byte[] buff = new byte[size];
			rng.GetBytes(buff);
			return Convert.ToBase64String(buff);
		}
		static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
		{
			HashAlgorithm algorithm = new SHA256Managed();

			byte[] plainTextWithSaltBytes =
			  new byte[plainText.Length + salt.Length];

			for (int i = 0; i < plainText.Length; i++)
			{
				plainTextWithSaltBytes[i] = plainText[i];
			}
			for (int i = 0; i < salt.Length; i++)
			{
				plainTextWithSaltBytes[plainText.Length + i] = salt[i];
			}

			return algorithm.ComputeHash(plainTextWithSaltBytes);
		}
	}
}