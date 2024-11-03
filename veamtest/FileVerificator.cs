using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace veamtest
{
    public static class FileVerificator
    {
        /// <summary>
        /// Compares two files to see if they are equal by calculating their MD5 hashes.
        /// </summary>
        /// <param name="filePath1"></param>
        /// <param name="filePath2"></param>
        /// <returns></returns>
        public static bool AreEqual(string filePath1, string filePath2)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream1 = File.OpenRead(filePath1))
                using (var stream2 = File.OpenRead(filePath2))
                {
                    byte[] hash1 = md5.ComputeHash(stream1);
                    byte[] hash2 = md5.ComputeHash(stream2);

                    return hash1.SequenceEqual(hash2);
                }
            }
        }
    }
}