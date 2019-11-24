using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Core.HashService.Md5HashService
{
    public class Md5HashGeneratorService : IHashGeneratorService
    {
        public string GenerateHash(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                byte[] btr = Encoding.UTF8.GetBytes(text);

                btr = md5.ComputeHash(btr);

                StringBuilder sb = new StringBuilder();

                foreach (byte ba in btr)
                {
                    sb.Append(ba.ToString("x2").ToLower());
                }

                return sb.ToString();
            }
        }

        public bool CompareHash(string hashedText, string text)
        {
            return GenerateHash(text) == hashedText;
        }
    }
}
