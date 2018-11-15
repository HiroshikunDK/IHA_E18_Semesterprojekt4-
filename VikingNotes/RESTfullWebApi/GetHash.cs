using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace RESTfullWebApi
{
    public static class GetHash
    {
        public static string SHA256(string stringToHash)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
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
    }
}