using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SM.SmartInfo.Utils
{
    public static class SHA
    {
        private const string _key = "RmxleEludmVudG9yeUBBdXRoZW50aWNhdGlvbg==";
        public static byte[] Hash(string value, byte[] salt)
        {
            return Hash(Encoding.UTF8.GetBytes(value), salt);
        }

        public static byte[] Hash(byte[] value, byte[] salt)
        {
            byte[] saltedValue = value.Concat(salt).ToArray();
            return saltedValue;
        }

        public static string GenerateSHA256String(string inputString)
        {
            byte[] salt = Encoding.UTF8.GetBytes(_key);
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Hash(inputString, salt);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        public static string GenerateSHA512String(string inputString)
        {
            byte[] salt = Encoding.UTF8.GetBytes(_key);
            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Hash(inputString, salt);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

    }
}
