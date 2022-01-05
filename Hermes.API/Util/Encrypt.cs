using System.Security.Cryptography;
using System.Text;

namespace Hermes.API.Util
{
    public class Encrypt
    {
        public static string GetSha256(string plainText)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(plainText));
            for (int i = 0; i < stream.Length; i++)
            {
                sb.AppendFormat("{0:x2}", stream[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Ref out of Plain text hashed and Salt hashed
        /// </summary>
        /// <param name="plainText">Text to encrypt</param>
        /// <param name="hashedText">Ref out var</param>
        /// <param name="hashedSalt">Ref out var</param>

        //public static  void GetHMACSHA512(string plainText, out string hashedText, out string hashedSalt)
        //{
        //    using(var hmac = new HMACSHA512())
        //    {
        //        var saltStream= hmac.Key;
        //        var passwordStream = hmac.ComputeHash(Encoding.UTF8.GetBytes(plainText));

        //        var sb = new StringBuilder();

        //        for (int i = 0; i < saltStream.Length; i++)
        //        {
        //            sb.AppendFormat("{0:x2}", saltStream[i]);
        //        }

        //        hashedSalt = sb.ToString();

        //        sb.Clear();

        //        for (int i = 0; i < passwordStream.Length; i++)
        //        {
        //            sb.AppendFormat("{0:x2}", passwordStream[i]);
        //        }

        //        hashedText = sb.ToString();
        //    }
        //}
    }
}
