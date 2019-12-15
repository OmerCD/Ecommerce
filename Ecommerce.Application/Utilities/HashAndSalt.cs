﻿using System.Collections.Generic;
using System.Security.Cryptography;

namespace Ecommerce.Application.Utilities
{
    public static class HashAndSalt
    {
        private static readonly HashAlgorithm Algorithm;
        private static readonly RNGCryptoServiceProvider RngCryptoServiceProvider;

        static HashAndSalt()
        {
            Algorithm = new SHA256Managed();
            RngCryptoServiceProvider = new RNGCryptoServiceProvider();
        }

        public static byte[] GenerateSaltedHash(IReadOnlyList<byte> plainText, IReadOnlyList<byte> salt)
        {

            var plainTextWithSaltBytes = 
                new byte[plainText.Count + salt.Count];

            for (int i = 0; i < plainText.Count; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Count; i++)
            {
                plainTextWithSaltBytes[plainText.Count + i] = salt[i];
            }

            return Algorithm.ComputeHash(plainTextWithSaltBytes);            
        }
        public static byte[] GetSalt(int maximumSaltLength)
        {
            var salt = new byte[maximumSaltLength];
            using (var random = RngCryptoServiceProvider)
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }
        public static bool CompareByteArrays(this byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (var i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}