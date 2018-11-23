using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace RESTfullWebApi
{
    public static class GetHash
    {
        public static string[] SHA256(string stringToHash, string salt = null)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            string[] returnStrings = new string[2];
            if (salt == null)
            {
                returnStrings[1] = GenerateSalt();
            }
            else
            {
                returnStrings[1] = salt;
            }
            
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(returnStrings[1] + stringToHash));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            returnStrings[0] = hash.ToString();
            return returnStrings;
        }

        public static string SHA1(string stringToHash)
        {
            var crypt = new System.Security.Cryptography.SHA1Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        static Random rand = new Random();

        public const string Alphabet =
            "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string GenerateSalt()
        {
            int size = rand.Next(5, 20);
            char[] chars = new char[size];
            for (int i = 0; i < size; i++)
            {
                chars[i] = Alphabet[rand.Next(Alphabet.Length)];
            }
            return new string(chars);
        }
    }
}